namespace ZoniSMLibrary.Models;
public class Setting
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BsonId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public int ParentId { get; set; } = 0;
    public string ModifiedBy { get; set; } = "SYSTEM";
    public DateTime ModifiedDate { get; set; }
    public bool AdvancedSetting { get; set; } = false;
    public bool ShowInSettings { get; set; } = false;
    public DateTime AddedDate { get; set; } = DateTime.Now;
    public string AppVersion { get; set; } = "0.0.1";
}
