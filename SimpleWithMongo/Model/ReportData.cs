using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SimpleWithMongo.Model;

public class ReportData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string Temperture { get; set; } = string.Empty;
    public string Gasdata { get; set; } = string.Empty;
    public string humidity { get; set; } = string.Empty;
    public bool Raindrop { get; set; }
}
