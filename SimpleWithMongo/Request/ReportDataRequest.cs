namespace SimpleWithMongo.Request;

public class ReportDataRequest
{
    public string Temperture { get; set; } = string.Empty;
    public string Gasdata { get; set; } = string.Empty;
    public string humidity { get; set; } = string.Empty;
    public bool Raindrop { get; set; }
}
