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
    }
}
