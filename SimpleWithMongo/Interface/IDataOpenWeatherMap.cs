using SimpleWithMongo.Request;

namespace SimpleWithMongo.Interface;

public interface IDataOpenWeatherMap
{
    Task<OpenWeatherMapRequest> GetData();

    Task UpdateData();
}
