using MongoDB.Driver;
using SimpleWithMongo.DbConText;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;
using SimpleWithMongo.Services;

namespace SimpleWithMongo.config;

public static class DataBaseConfig
{
    public static void ConfigureDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDbContext>(provide =>
        {
            var connectionString = configuration.GetValue<string>("MongoConnection:ConnectionString");
            var DataName = configuration.GetValue<string>("MongoConnection:DatabaseName");
            return new MongoDbContext(connectionString!, DataName!);
        });
        services.AddHttpClient();
        services.AddScoped<IAccountService,AccountService>();
        services.AddScoped<IReportData, ReportDataService>();
        services.AddScoped<IDataOpenWeatherMap, DataOpenWeatherMap>();
        services.AddScoped<IDataNow, DataDayNowService>();
        services.AddScoped<INotification, NotificationService>();
    }
}
