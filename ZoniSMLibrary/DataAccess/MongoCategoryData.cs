using Microsoft.Extensions.Caching.Memory;

namespace ZoniSMLibrary.DataAccess;
public class MongoCategoryData : ICategoryData
{
    private readonly IMemoryCache _cache;
    private readonly IMongoCollection<Category> _categories;
    private const string CacheName = "CategoryData";

    public MongoCategoryData(IDbConnection db, IMemoryCache cache)
    {
        _cache = cache;
        _categories = db.CategoryCollection;
    }

    public async Task<List<Category>> GetAllCategoriesAsync(bool ignoreCache = false)
    {
        List<Category>? output = null;

        if (!ignoreCache)
        {
            output = _cache.Get<List<Category>>(CacheName);
        }

        if (output == null)
        {
            var results = await _categories.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _categories.InsertOneAsync(category);
    }

    public async Task CreateMultipleCategoriesAsync(IEnumerable<Category> categories)
    {
        await _categories.InsertManyAsync(categories);
    }

    public async Task RemoveCategoryAsync(Category category)
    {
        var deleteFilter = Builders<Category>.Filter.Eq("BsonId", category.BsonId);
        await _categories.DeleteOneAsync(deleteFilter);
        _cache.Remove(CacheName);
    }

    public async Task<Category> GetCategoryByNameAsync(string categoryName)
    {
        var output = _cache.Get<Category>(CacheName);
        if (output is null)
        {
            var results = await _categories.FindAsync(c => c.Name == categoryName);
            output = results.FirstOrDefault();

            _cache.Set(CacheName, output, TimeSpan.FromDays(1));
        }

        return output;
    }

    public async Task UpdateCategoriesAsync(IEnumerable<Category> categories)
    {
        foreach (var category in categories)
        {
            await _categories.ReplaceOneAsync(c => c.BsonId == category.BsonId, category);
        }

        _cache.Remove(CacheName);
    }

    public async Task<int> GetTotalCategoryCountAsync()
    {
        var results = await GetAllCategoriesAsync(false);

        return results.Count();
    }
}
