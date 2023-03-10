namespace BlepItLibrary.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName => FirstName + " " + LastName;
    public string EmailAddress { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoggedInDate { get; set; }
    public VIPStatus VIPStatus { get; set; } = VIPStatus.None;
    public UserSettings UserSettings { get; set; }
    public byte[] ProfilePicture { get; set; }
    public List<Notification> Notifications { get; set; }
    public List<string> CreatedPrompts { get; set; } = new List<string>();
    public List<string> FavouritePrompts { get; set; } = new List<string>();

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
    Paid,
    Admin
}
