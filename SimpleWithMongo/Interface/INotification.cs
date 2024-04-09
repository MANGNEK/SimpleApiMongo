using SimpleWithMongo.Model;

namespace SimpleWithMongo.Interface;

public interface INotification
{
    Task<List<NotifineModel>> GetAll();
    Task Create(NotifineModel notifine);
    Task DataInput(double Temperture , double Gas , bool Rain);
}
