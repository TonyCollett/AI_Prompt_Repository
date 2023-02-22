namespace BlepItLibrary.Models;

public class UserSettings
{
    public bool IsUserArchived { get; set; } = false;
    public bool IsUserEnabled { get; set; } = true;
    public string PreferredLanguage { get; set; }
    public bool IsUserVIP { get; set; } = false;
}
