using Microsoft.AspNetCore.Mvc;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Request;

namespace SimpleWithMongo.Controllers;

public class ReportDataController : ControllerBase
{
    private readonly IReportData reportData;
    private readonly IDataOpenWeatherMap weatherMap;
    private readonly INotification notification;
    public ReportDataController(IReportData reportData, IDataOpenWeatherMap weatherMap, INotification notification)
    {
        this.reportData = reportData;
        this.weatherMap = weatherMap;
        this.notification = notification;
    }

    [HttpPost]
    [Route("createReport")]
    public async Task CreateData([FromBody]ReportDataRequest request)
    {
      await reportData.Create(request);
    }
    [HttpPut]
    [Route("updateReport")]
    public async Task UpdateData([FromBody] ReportDataRequest request)
    {
        double tem = double.Parse(request.Temperture);
        double gas = double.Parse(request.Gasdata);
        await reportData.Update(request);
        await notification.DataInput(tem,gas,request.Raindrop);
    }
    [HttpGet]
    [Route("OpenMap")]
    public async Task<OpenWeatherMapRequest> Getdat()
    {
        await weatherMap.UpdateData();
        return await weatherMap.GetData();
    }
}
