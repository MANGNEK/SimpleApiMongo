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
        Console.WriteLine("có nek");
        await reportData.Update(request);
        Console.WriteLine(" data tem : "+ double.Parse(request.Temperture));
        if(double.Parse(request.Temperture) > 30) await notification.Create(new Model.NotifineModel { Note = "Hight Temperture ", TypeLog = "Value : " + request.Temperture});
        if(request.Raindrop) await notification.Create(new Model.NotifineModel { Note = "Have Rain Drop in You House ", TypeLog = "Value : True" });
        if(double.Parse(request.Gasdata)> 400 ) await notification.Create(new Model.NotifineModel { Note = "Maybe Have Gas Leak In You House", TypeLog = "Value : "+request.Gasdata });
    }
    [HttpGet]
    [Route("OpenMap")]
    public async Task<OpenWeatherMapRequest> Getdat()
    {
        await weatherMap.UpdateData();
        return await weatherMap.GetData();
    }
}
