namespace ZoniSMLibrary.Models;

public class Ticket
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public User CreatedBy { get; set; }
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
    public User LastUpdatedBy { get; set; }
    public User? AffectedUser { get; set; }
    public User AssignedUser { get; set; }
    public bool Archived { get; set; } = false;
    public Category Category { get; set; }
    public string OwnerNotes { get; set; }
    public IEnumerable<byte[]> Attachments { get; set; }
}