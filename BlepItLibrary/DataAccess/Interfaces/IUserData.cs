namespace BlepItLibrary.DataAccess.Interfaces;

public interface IUserData
{
    Task CreateUserAsync(User user);
    Task CreateMultipleUsersAsync(IEnumerable<User> users);
    Task<User> GetUserByBsonIdAsync(string id);
    Task<bool> IsUserRegisteredAsync(string nameIdentifier);
    Task<string> GetUserByUsernameAsync(string username);
    Task<User> GetUserFromAuthentication(string objectId);
    Task<List<User>> GetAllUsersAsync(bool ignoreCache = false);
    Task ToggleFavouriteOnUserAsync(User user, string promptId);
    Task UpdateUserAsync(User user);
    Task IssueNotificationAsync(User user, Notification notification);
}