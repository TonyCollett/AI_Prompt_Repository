using Microsoft.Extensions.Configuration;

namespace BlepItLibrary.DataAccess;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private readonly string _connectionId = "MongoDB";
    public string DbName { get; private set; }
    public string CommentCollectionName { get; private set; } = "comments";
    public string NotificationCollectionName { get; private set; } = "notifications";
    public string UserCollectionName { get; private set; } = "users";
    public string PromptCollectionName { get; private set; } = "prompts";
    public MongoClient Client { get; private set; }
    public IMongoCollection<Comment> CommentCollection { get; private set; }
    public IMongoCollection<Notification> NotificationCollection { get; private set; }
    public IMongoCollection<User> UserCollection { get; private set; }
    public IMongoCollection<Prompt> PromptCollection { get; private set; }

    public DbConnection(IConfiguration config)
    {
        try
        {
            _config = config;
            Client = new MongoClient(_config.GetConnectionString(_connectionId));
            DbName = _config["DatabaseName"] ?? "BlepIt";
            _db = Client.GetDatabase(DbName);

            CommentCollection = _db.GetCollection<Comment>(CommentCollectionName);
            NotificationCollection = _db.GetCollection<Notification>(NotificationCollectionName);
            UserCollection = _db.GetCollection<User>(UserCollectionName);
            PromptCollection = _db.GetCollection<Prompt>(PromptCollectionName);
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    public async Task DropAllDataCollectionsAsync()
    {
        await _db.DropCollectionAsync(CommentCollectionName);
        await _db.DropCollectionAsync(NotificationCollectionName);
        await _db.DropCollectionAsync(UserCollectionName);
        await _db.DropCollectionAsync(PromptCollectionName);
    }
}

