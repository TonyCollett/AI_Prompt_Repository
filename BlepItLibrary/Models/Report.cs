namespace BlepItLibrary.Models;

public class Report
{
    public string PromptId { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public User? ReportedBy { get; set; }
    public string? IpAddress { get; set; }
}