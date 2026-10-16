public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category, CancellationToken ct = default);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken ct = default);
    Task<List<Category>> GetAllCategoriesAsync(CancellationToken ct = default);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken ct = default);
    Task<List<Category>> GetPaginatedCategoriesAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    Task<bool> RestoreCategoryAsync(int id, CancellationToken ct = default);
    Task<Category?> UpdateCategoryAsync(int id, Category category, CancellationToken ct = default);
}