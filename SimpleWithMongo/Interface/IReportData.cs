using SimpleWithMongo.Request;

namespace SimpleWithMongo.Interface;

public interface IReportData
{
    Task<bool> Create(ReportDataRequest request);
    Task<bool> Update(ReportDataRequest request);
}
