using MongoDB.Driver;

namespace SimpleWithMongo.Interface;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string collectionName);
    //Task SaveChangeAsync(CancellationToken cancellationToken = default(CancellationToken));
}
