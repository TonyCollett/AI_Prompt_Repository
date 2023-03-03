
namespace BlepItLibrary.DataAccess.Interfaces;

public interface IPromptData
{
    Task<List<Prompt>> GetAllActivePromptsAsync();
    Task ToggleFavouriteOnPromptAsync(Prompt prompt, string userId);
    Task<List<Prompt>> GetPromptsForPageAsync(int page, int promptsPerPage);
    Task<long> CountAllActivePromptsAsync();
    Task<IEnumerable<Prompt>> SearchForPromptByTextAsync(string text);
    Task<Prompt> GetPromptAsync(string id);
    Task<List<Prompt>> GetUserCreatedPromptsAsync(User user);
    Task UpdatePromptAsync(Prompt prompt);
    Task CreatePromptAsync(Prompt prompt);
    Task CreateMultiplePromptsAsync(IEnumerable<Prompt> prompts);
    Task<List<Prompt>> GetPromptsByIdsAsync(IEnumerable<string> promptIds);
}