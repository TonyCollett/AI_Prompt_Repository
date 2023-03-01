namespace BlepItLibrary.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName => FirstName + " " + LastName;
    public string EmailAddress { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoggedInDate { get; set; }
    public VIPStatus VIPStatus { get; set; } = VIPStatus.None;
    public UserSettings UserSettings { get; set; }
    public byte[] ProfilePicture { get; set; }
    public IEnumerable<Notification> Notifications { get; set; }
    public IEnumerable<SimplePrompt> CreatedPrompts { get; set; }
    public IEnumerable<SimplePrompt> FavouritePrompts { get; set; }

    public override bool Equals(object? o)
    {
        User? other = o as User;
        return other?.DisplayName == DisplayName;
    }

    public override int GetHashCode() => DisplayName?.GetHashCode() ?? 0;

    public override string ToString() => DisplayName;


}

public enum VIPStatus
{
    None,
    Paid
}
