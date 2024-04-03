using Mapster;
using MongoDB.Driver;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;
using SimpleWithMongo.Request;

namespace SimpleWithMongo.Services;

public class DataDayNowService : IDataNow
{
    private readonly IMongoCollection<DataDayNowModel> _dataNow;
    public DataDayNowService(IMongoDbContext Context) {
        _dataNow = Context.GetCollection<DataDayNowModel>("DataDayNowModel");
    }
    public async Task Inser(DataDayNowModel dataDayNow)
    {
        await _dataNow.InsertOneAsync(dataDayNow);
    }

    public async Task Update(DataDayNowModel dataDayNow)
    {
            var filter = Builders<DataDayNowModel>.Filter.Empty;
            var count = await _dataNow.CountDocumentsAsync(filter);
            if (count > 0)
            {
                var update = Builders<DataDayNowModel>.Update
              .Set(x => x.Temperture, dataDayNow.Temperture)
              .Set(x => x.Humidity, dataDayNow.Humidity)
              .Set(x => x.WindSpeed, dataDayNow.WindSpeed)
              .Set(x => x.Cloud, dataDayNow.Cloud.ToString());
                var result = await _dataNow.UpdateManyAsync(filter, update);
            return;
            }
            _dataNow.InsertOne(_dataNow.Adapt<DataDayNowModel>());         
      
    }
}
