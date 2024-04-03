using Quartz;
using SimpleWithMongo.Interface;

namespace SimpleWithMongo.Cronjob;

public class JodGetData(IDataOpenWeatherMap weatherMap) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await weatherMap.UpdateData();
    }
}
