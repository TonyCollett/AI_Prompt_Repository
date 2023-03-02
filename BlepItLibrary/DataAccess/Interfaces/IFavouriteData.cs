namespace BlepItLibrary.DataAccess.Interfaces;

public interface IFavouriteData
{
    Task<List<Favourite>> GetAllFavouritesAsync();
    Task<List<Favourite>> GetFavouritesByPromptIdAsync(string promptId);
    Task<List<Favourite>> GetFavouritesByUserIdAsync(string userId);
    Task CreateFavouriteAsync(Favourite favourite);
    Task CreateMultipleFavouritesAsync(IEnumerable<Favourite> favourites);
    Task<ReplaceOneResult> UpdateFavouriteAsync(Favourite favourite);
}