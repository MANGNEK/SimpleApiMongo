using Mapster;
using MongoDB.Driver;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;
using SimpleWithMongo.Request;

namespace SimpleWithMongo.Services
{
    public class ReportDataService : IReportData
    {
        private readonly IMongoCollection<ReportData> _reports;
        
        public ReportDataService(IMongoDbContext mongoClient)
        {
            _reports = mongoClient.GetCollection<ReportData>("ReportData");
        }
        public async Task<bool> Create(ReportDataRequest request)
        {
          await _reports.InsertOneAsync(request.Adapt<ReportData>());
            return true;
        }

        public async Task<bool> Update(ReportDataRequest request)
        {
            var filter = Builders<ReportData>.Filter.Empty;
            var count = await _reports.CountDocumentsAsync(filter);
            if(count > 0)
            {
                var update = Builders<ReportData>.Update
              .Set(x => x.Temperture, request.Temperture)
              .Set(x => x.Gasdata, request.Gasdata)
              .Set(x => x.humidity, request.humidity)
              .Set(x => x.Raindrop, request.Raindrop);
              var result = await _reports.UpdateManyAsync(filter, update);
                return true;
            }
            _reports.InsertOne(request.Adapt<ReportData>());
             return true;
        }
    }
}
