using MongoDB.Driver;
using SimpleWithMongo.Interface;

namespace SimpleWithMongo.DbConText;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(string connection , string databaseName)
    {
        var client = new MongoClient(connection);
        _database = client.GetDatabase(databaseName);
    }
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

}
