namespace ZoniSMLibrary.Models;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get => FirstName + " " + LastName; }
    public string Initials { get => string.Concat(FirstName.AsSpan(0, 1), LastName.AsSpan(0, 1)); }
    public string EmailAddress { get; set; }
    public string EmailAddress2 { get; set; }
    public string PhoneNumber { get; set; }
    public string PhoneNumber2 { get; set; }
    public string Department { get; set; }
    public string EmployeeId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    public string Office { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoggedInDate { get; set; }
    public UserType UserType { get; set; } = UserType.EndUser;
    public UserSettings UserSettings { get; set; }
    public List<Ticket> CreatedTickets { get; set; } = new();
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
