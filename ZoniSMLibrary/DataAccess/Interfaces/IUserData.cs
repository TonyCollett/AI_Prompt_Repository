namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface IUserData
{
    Task CreateUserAsync(User user);
    Task CreateMultipleUsersAsync(IEnumerable<User> users);
    Task<User> GetUserByBsonIdAsync(string id);
    Task<string> GetUserByUsernameAsync(string username);
    Task<User> GetUserFromAuthentication(string objectId);
    Task<List<User>> GetAllUsersAsync(bool ignoreCache = false);
    Task UpdateUserAsync(User user);
    Task IssueNotificationAsync(User user, Notification notification);
}