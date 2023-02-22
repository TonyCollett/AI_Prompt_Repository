namespace ZoniSMLibrary.Models;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public string Contents { get; set; }
    public string Type { get; set; }
    public DateTime Expires { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public User ForUser { get; set; }

    public override bool Equals(object? o)
    {
        var other = o as Notification;
        return other?.Title == Title;
    }

    public override int GetHashCode() => Title?.GetHashCode() ?? 0;

    public override string ToString() => Title;
}