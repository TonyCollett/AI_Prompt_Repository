
namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface ICategoryData
{
    Task CreateCategoryAsync(Category category);
    Task CreateMultipleCategoriesAsync(IEnumerable<Category> categories);
    Task<List<Category>> GetAllCategoriesAsync(bool ignoreCache = false);
    Task<Category> GetCategoryByNameAsync(string categoryName);
    Task RemoveCategoryAsync(Category category);
    Task UpdateCategoriesAsync(IEnumerable<Category> categories);
    Task<int> GetTotalCategoryCountAsync();
}