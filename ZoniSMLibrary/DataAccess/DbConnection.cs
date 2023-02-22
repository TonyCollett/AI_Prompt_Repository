using Microsoft.Extensions.Configuration;

namespace ZoniSMLibrary.DataAccess;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private readonly string _connectionId = "MongoDB";
    public string DbName { get; private set; }
    public string CategoryCollectionName { get; private set; } = "categories";
    public string CommentCollectionName { get; private set; } = "comments";
    public string NotificationCollectionName { get; private set; } = "notifications";
    public string SettingsCollectionName { get; private set; } = "settings";
    public string StatusCollectionName { get; private set; } = "statuses";
    public string UserCollectionName { get; private set; } = "users";
    public string TicketCollectionName { get; private set; } = "tickets";

    public MongoClient Client { get; private set; }
    public IMongoCollection<Category> CategoryCollection { get; private set; }
    public IMongoCollection<Comment> CommentCollection { get; private set; }
    public IMongoCollection<Notification> NotificationCollection { get; private set; }
    public IMongoCollection<Setting> SettingsCollection { get; private set; }
    public IMongoCollection<Status> StatusCollection { get; private set; }
    public IMongoCollection<User> UserCollection { get; private set; }
    public IMongoCollection<Ticket> TicketCollection { get; private set; }

    public DbConnection(IConfiguration config)
    {
        try
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString(_connectionId));
            DbName = _config["DatabaseName"] ?? "ZoniSM";
            _db = Client.GetDatabase(DbName);

            CategoryCollection = _db.GetCollection<Category>(CategoryCollectionName);
            CommentCollection = _db.GetCollection<Comment>(CommentCollectionName);
            NotificationCollection = _db.GetCollection<Notification>(NotificationCollectionName);
            SettingsCollection = _db.GetCollection<Setting>(SettingsCollectionName);
            StatusCollection = _db.GetCollection<Status>(StatusCollectionName);
            UserCollection = _db.GetCollection<User>(UserCollectionName);
            TicketCollection = _db.GetCollection<Ticket>(TicketCollectionName);
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    public async Task DropAllDataCollectionsAsync()
    {
        await _db.DropCollectionAsync(CategoryCollectionName);
        await _db.DropCollectionAsync(CommentCollectionName);
        await _db.DropCollectionAsync(NotificationCollectionName);
        await _db.DropCollectionAsync(StatusCollectionName);
        await _db.DropCollectionAsync(UserCollectionName);
        await _db.DropCollectionAsync(TicketCollectionName);
    }
}

