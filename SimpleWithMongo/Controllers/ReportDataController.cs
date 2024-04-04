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
        await reportData.Update(request);
        if(double.Parse(request.Temperture) > 36) await notification.Create(new Model.NotifineModel { Note = request.Temperture , TypeLog = "Hight Temperture!!! , Data Temperture: "+request.Temperture });
        if(request.Raindrop) await notification.Create(new Model.NotifineModel { Note = "Have Rain Drop in You House ", TypeLog = "Raind Drop!!!" });
        if(double.Parse(request.Gasdata)> 400 ) await notification.Create(new Model.NotifineModel { Note = "Maybe Have Gas Leak In You House", TypeLog = "Gas Leak!!!, Data Gas : "+request.Gasdata });
    }
    [HttpGet]
    [Route("OpenMap")]
    public async Task<OpenWeatherMapRequest> Getdat()
    {
        await weatherMap.UpdateData();
        return await weatherMap.GetData();
    }
}
