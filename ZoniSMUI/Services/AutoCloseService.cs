namespace ZoniSMUI.Services;

public class AutoCloseService : BackgroundService
{
    private readonly ITicketData _ticketData;
    private readonly ISettingsData _settingsData;

    public AutoCloseService(IServiceProvider serviceProvider)
    {
        _ticketData = serviceProvider.GetRequiredService<ITicketData>();
        _settingsData = serviceProvider.GetRequiredService<ISettingsData>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Setting closeTimerCheck = await _settingsData.GetSettingAsync("CloseTimerCheckMinutes");
        PeriodicTimer _timer = new(TimeSpan.FromMinutes(10));

        //Setting workItemAutoCloseDays = await _settingsData.GetSettingAsync("WorkItemAutoCloseDays");

        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            await _ticketData.CloseTicketsAsync(7);
        }
    }
}
