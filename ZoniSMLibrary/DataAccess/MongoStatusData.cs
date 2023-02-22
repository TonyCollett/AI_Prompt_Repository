using Microsoft.Extensions.Caching.Memory;

namespace ZoniSMLibrary.DataAccess;
public class MongoStatusData : IStatusData
{
    private readonly IDbConnection _db;
    private readonly IMongoCollection<Status> _statuses;
    private readonly IMemoryCache _cache;
    private const string CacheName = "StatusData";

    public MongoStatusData(IDbConnection db, IMemoryCache cache)
    {
        _cache = cache;
        _statuses = db.StatusCollection;
        _db = db;
    }

    public async Task<List<Status>> GetAllStatusesAsync(bool ignoreCache = false)
    {
        List<Status>? output = null;

        if (!ignoreCache)
        {
            output = _cache.Get<List<Status>>(CacheName);
        }

        if (output == null)
        {
            var results = await _statuses.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public async Task CreateStatusAsync(Status status)
    {
        await _statuses.InsertOneAsync(status);
        _cache.Remove(CacheName);
    }

    public async Task CreateMultipleStatusesAsync(IEnumerable<Status> statuses)
    {
        await _statuses.InsertManyAsync(statuses);
        _cache.Remove(CacheName);
    }

    public async Task RemoveStatusAsync(Status status)
    {
        var deleteFilter = Builders<Status>.Filter.Eq("BsonId", status.BsonId);
        await _statuses.DeleteOneAsync(deleteFilter);
        _cache.Remove(CacheName);
    }

    public async Task<Status> GetStatusByNameAsync(string statusName)
    {
        var output = _cache.Get<Status>(CacheName + statusName);
        if (output is null)
        {
            var results = await _statuses.FindAsync(s => s.Name == statusName);
            output = results.FirstOrDefault();

            _cache.Set(CacheName + statusName, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public async Task UpdateStatusesAsync(IEnumerable<Status> statuses)
    {
        foreach (var status in statuses)
        {
            await _statuses.ReplaceOneAsync(s => s.BsonId == status.BsonId, status);
        }

        _cache.Remove(CacheName);
    }
}
