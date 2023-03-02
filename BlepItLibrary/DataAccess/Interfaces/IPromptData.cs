
namespace BlepItLibrary.DataAccess.Interfaces;

public interface IPromptData
{
    Task<List<Prompt>> GetAllActivePromptsAsync();
    Task ToggleFavouriteOnPromptAsync(Prompt prompt, User user);
    Task<List<Prompt>> GetPromptsForPageAsync(int page);
    Task<Prompt> GetPromptAsync(string id);
    Task<List<Prompt>> GetUserCreatedPromptsAsync(User user);
    Task UpdatePromptAsync(Prompt prompt);
    Task CreatePromptAsync(Prompt prompt);
    Task CreateMultiplePromptsAsync(IEnumerable<Prompt> prompts);
}