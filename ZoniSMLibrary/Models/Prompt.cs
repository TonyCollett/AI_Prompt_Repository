namespace BlepItLibrary.Models;

public class Prompt
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public User CreatedBy { get; set; }
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
    public User LastUpdatedBy { get; set; }
    public bool Archived { get; set; }
    public IEnumerable<byte[]> Attachments { get; set; } = new List<byte[]>(5);
}