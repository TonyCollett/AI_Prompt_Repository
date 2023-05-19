namespace BlepItLibrary.Models;

public class Attachment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string PromptId { get; set; }
    public IEnumerable<byte[]> Files { get; set; }
}
