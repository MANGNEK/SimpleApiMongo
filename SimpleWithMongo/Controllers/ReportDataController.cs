using Microsoft.AspNetCore.Mvc;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Request;

namespace SimpleWithMongo.Controllers;

public class ReportDataController : ControllerBase
{
    private readonly IReportData reportData;
    private readonly IDataOpenWeatherMap weatherMap;
    public ReportDataController(IReportData reportData, IDataOpenWeatherMap weatherMap)
    {
        this.reportData = reportData;
        this.weatherMap = weatherMap;
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
    }
    [HttpGet]
    [Route("OpenMap")]
    public async Task<OpenWeatherMapRequest> Getdat()
    {
        await weatherMap.UpdateData();
        return await weatherMap.GetData();
    }
}
