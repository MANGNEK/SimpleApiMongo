using Microsoft.AspNetCore.Mvc;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Request;

namespace SimpleWithMongo.Controllers;

public class ReportDataController : ControllerBase
{
    private readonly IReportData reportData;
    public ReportDataController(IReportData reportData)
    {
        this.reportData = reportData;
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
}
