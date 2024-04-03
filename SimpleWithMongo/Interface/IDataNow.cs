using SimpleWithMongo.Model;

namespace SimpleWithMongo.Interface;

public interface IDataNow
{
    Task Inser(DataDayNowModel dataDayNow);
    Task Update(DataDayNowModel dataDayNow);

}
