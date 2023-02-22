namespace BlepItLibrary.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get => FirstName + " " + LastName; }
    public string EmailAddress { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoggedInDate { get; set; }
    public UserType UserType { get; set; } = UserType.EndUser;
    public UserSettings UserSettings { get; set; }
    public List<Prompt> CreatedPrompts { get; set; } = new();
    public List<Notification> Notifications { get; set; }
    public byte[] ProfilePicture { get; set; }

    public override bool Equals(object? o)
    {
        var other = o as User;
        return other?.DisplayName == DisplayName;
    }

    public override int GetHashCode() => DisplayName?.GetHashCode() ?? 0;

    public override string ToString() => DisplayName;
}

public enum UserType
{
    EndUser,
    Analyst,
    Admin
}
