﻿namespace BlepItLibrary.DataAccess.Interfaces;

public interface IDbConnection
{
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<Comment> CommentCollection { get; }
    string CommentCollectionName { get; }
    IMongoCollection<Favourite> FavouriteCollection { get; }
    string FavouriteCollectionName { get; }
    IMongoCollection<Notification> NotificationCollection { get; }
    string NotificationCollectionName { get; }
    IMongoCollection<Prompt> PromptCollection { get; }
    string PromptCollectionName { get; }
    IMongoCollection<User> UserCollection { get; }
    string UserCollectionName { get; }

    Task DropAllDataCollectionsAsync();
}