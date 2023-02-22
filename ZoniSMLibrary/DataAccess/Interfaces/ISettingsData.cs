
namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface ISettingsData
{
    Task<List<Setting>> GetAllSettingsAsync();
    Task<Setting> GetSettingAsync(string name);

    /// <summary>
    /// Gets and returns the Ticket ID from Settings database. 
    /// Increments the ID by 1 and re-stores the new ID ready for the next Ticket.
    /// </summary>
    /// <returns>String of numbers in format "#####"</returns>
    Task<string> GetTicketIdAsStringAsync();

    Task UpdateSettingAsync(string settingName, string settingValue);
    Task CreateSettingAsync(string settingName, string initialValue, bool advancedSetting = false, bool showInSettings = false);

    Task InitialiseAllSettingsAsync();
}