using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace BlepItLibrary.DataAccess;
public class MongoNotificationData : INotificationData
{
    private readonly IDbConnection _db;
    private readonly IMongoCollection<Notification> _notifications;
    private readonly IMemoryCache _cache;
    private const string CacheName = "NotificationData";

    public MongoNotificationData(IDbConnection db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
        _notifications = db.NotificationCollection;
    }

    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        var output = _cache.Get<List<Notification>>(CacheName);
        if (output is null)
        {
            var results = await _notifications.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public async Task CreateNotificationAsync(Notification notification)
    {
        var client = _db.Client;
        using var session = await client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var notificationInTransaction = db.GetCollection<Notification>(_db.NotificationCollectionName);
            await notificationInTransaction.InsertOneAsync(notification);

            var usersInTransaction = db.GetCollection<User>(_db.UserCollectionName);
            notification.ForUser.Notifications.Add(notification);
            await usersInTransaction.ReplaceOneAsync(session, u => u.Id == notification.ForUser.Id, notification.ForUser);

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task CreateMultipleNotificationsAsync(IEnumerable<Notification> notifications)
    {
        await _notifications.InsertManyAsync(notifications);
    }

    public async Task<Notification> GetNotificationByNameAsync(string notificationTitle)
    {
        var output = _cache.Get<Notification>(CacheName + notificationTitle);
        if (output is null)
        {
            var results = await _notifications.FindAsync(s => s.Title == notificationTitle);
            output = results.FirstOrDefault();

            _cache.Set(CacheName + notificationTitle, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public Task DeleteNotificationAsync(string notification)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsReadNotificationAsync(string notification)
    {
        throw new NotImplementedException();
    }

    public Task GetNotificationsForLoggedInUser(User user)
    {
        throw new NotImplementedException();
    }
}
