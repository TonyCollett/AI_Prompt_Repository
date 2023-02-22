using Microsoft.Extensions.Caching.Memory;
using ZoniSMLibrary.DataAccess.Interfaces;

namespace ZoniSMLibrary.DataAccess;

public class MongoSettingsData : ISettingsData
{
    private readonly IMongoCollection<Setting> _settings;
    private readonly IMemoryCache _cache;
    private const string CacheName = "SettingsData";

    private const string LastUsedId = "LastUsedId";

    public MongoSettingsData(IDbConnection db, IMemoryCache cache)
    {
        _settings = db.SettingsCollection;
        _cache = cache;
    }

    public async Task<List<Setting>> GetAllSettingsAsync()
    {
        var results = await _settings.Find(_ => true).ToListAsync();
        return results;
    }

    public async Task<Setting> GetSettingAsync(string name)
    {
        var results = await _settings.Find(x => x.Name == name).FirstOrDefaultAsync();
        return results;
    }

    public async Task<string> GetTicketIdAsStringAsync()
    {
        Setting result = _settings.Find(x => x.Name == LastUsedId).FirstOrDefault();

        int nextId = int.Parse(result.Value);
        nextId++;
        await UpdateSettingAsync(LastUsedId, nextId.ToString("000000"));

        return result.Value;
    }

    public async Task CreateSettingAsync(string settingName, string initialValue, bool advancedSetting = false, bool showInSettings = false)
    {
        var setting = new Setting
        {
            Name = settingName,
            Value = initialValue,
            AdvancedSetting = advancedSetting,
            ShowInSettings = showInSettings,
            ModifiedBy = "System",
            AppVersion = "0.0.1"
        };

        InsertOneOptions options = new()
        {
            BypassDocumentValidation = false,
            Comment = "Test comment"
        };

        await _settings.InsertOneAsync(setting, options, default);
    }

    public async Task UpdateSettingAsync(string settingName, string settingValue)
    {
        var filter = Builders<Setting>.Filter.Eq("Name", settingName);
        var settingUpdate = Builders<Setting>.Update.Set("Value", settingValue);
        await _settings.UpdateOneAsync(filter, settingUpdate, new UpdateOptions { IsUpsert = true });
    }

    public async Task InitialiseAllSettingsAsync()
    {
        await _settings.DeleteManyAsync(_ => true);

        var settings = new List<Setting>
        {
            new Setting { Name = "LastUsedWIID", Value = "00000", AdvancedSetting = false, ShowInSettings = false, ModifiedBy = "System", AppVersion = "0.0.1" },
            new Setting { Name = "ApplicationName", Value = "ZoniSM", AdvancedSetting = false, ShowInSettings = true, ModifiedBy = "System", AppVersion = "0.0.1" },
            new Setting { Name = "LastNotificationCheck", Value = DateTime.Now.ToString(), AdvancedSetting = false, ShowInSettings = false, ModifiedBy = "System", AppVersion = "0.0.1" }
        };

        await _settings.InsertManyAsync(settings);
    }
}
