using MongoDB.Driver;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;

namespace SimpleWithMongo.Services
{
    public class NotificationService : INotification
    {
        private readonly IMongoCollection<NotifineModel> _notifications;
        public NotificationService(IMongoDbContext mongoDb) => _notifications = mongoDb.GetCollection<NotifineModel>("NotifineModel");

        public async Task Create(NotifineModel notifine)
        {
                await _notifications.InsertOneAsync(notifine);

        }
        public async Task<List<NotifineModel>> GetAll()
        {
           var list = await _notifications.Find(e => e.Id != null).ToListAsync();
           if(list.Count() > 0) return list;
           return new List<NotifineModel>();
        }
        public async Task DataInput(double Temperture , double Gas , bool Rain){
            if(Temperture > 40) await Create(new NotifineModel { Note = "Hight Temperture ", TypeLog = "Value : " + Temperture});
            if(Gas > 400) await Create(new NotifineModel { Note = "Maybe Have Gas Leak In You House", TypeLog = "Value : "+Gas });
            if(Rain) await Create(new NotifineModel{Note =" Have Rain", TypeLog = "Value : "+ Rain});
            Console.WriteLine("Oke !!!");
        }
    }
}
