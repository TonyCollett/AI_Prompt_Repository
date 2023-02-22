namespace BlepItLibrary.DataAccess.Interfaces;

public interface INotificationData
{
    Task CreateNotificationAsync(Notification notification);
    Task CreateMultipleNotificationsAsync(IEnumerable<Notification> notifications);
    Task<List<Notification>> GetAllNotificationsAsync();
    Task<Notification> GetNotificationByNameAsync(string notification);
    Task DeleteNotificationAsync(string notification);
    Task GetNotificationsForLoggedInUser(User user);
}