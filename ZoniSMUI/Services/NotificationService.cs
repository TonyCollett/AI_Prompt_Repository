namespace ZoniSMUI.Services;

public class NotificationService
{
    private readonly ISettingsData _settingsData;

    public NotificationService(IServiceProvider serviceProvider)
    {
        _settingsData = serviceProvider.GetRequiredService<ISettingsData>();
    }

    public void SendNotification(string message)
    {
        // code for sending notification goes here
    }

}
