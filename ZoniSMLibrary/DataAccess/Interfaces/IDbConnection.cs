namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface IDbConnection
{
    IMongoCollection<Category> CategoryCollection { get; }
    string CategoryCollectionName { get; }
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<Comment> CommentCollection { get; }
    IMongoCollection<Notification> NotificationCollection { get; }
    IMongoCollection<Setting> SettingsCollection { get; }
    IMongoCollection<Status> StatusCollection { get; }
    IMongoCollection<User> UserCollection { get; }
    IMongoCollection<Ticket> TicketCollection { get; }
    string CommentCollectionName { get; }
    string NotificationCollectionName { get; }
    string SettingsCollectionName { get; }
    string StatusCollectionName { get; }
    string UserCollectionName { get; }
    string TicketCollectionName { get; }

    Task DropAllDataCollectionsAsync();
}