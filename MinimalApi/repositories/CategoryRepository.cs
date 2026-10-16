using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repositorio para la conexión con el contexto por parte de las categorías.
/// </summary>
/// <param name="_context">Contexto configurado con conexión a PostgreSQL.</param>
public class CategoryRepository(
    AppDbContext _context
) : ICategoryRepository
{
    /// <inheritdoc cref="ICategoryRepository.CreateCategoryAsync(Category, CancellationToken)"/>
    public async Task<Category> CreateCategoryAsync(
        Category category,
        CancellationToken ct = default
    )
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(ct);

        return category;
    }

    /// <inheritdoc cref="ICategoryRepository.UpdateCategoryAsync(int, Category, CancellationToken)"/>
    public async Task<Category?> UpdateCategoryAsync(
        int id,
        Category category,
        CancellationToken ct = default
    )
    {
        var response = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, ct);

        if (response is null)
            return null;

        response.Name = category.Name;
        response.UpdateAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(ct);

        return response;
    }

    /// <inheritdoc cref="ICategoryRepository.GetAllCategoriesAsync(CancellationToken)"/>
    public Task<List<Category>> GetAllCategoriesAsync(
        CancellationToken ct = default
    )
    {
        return _context.Categories
            .AsNoTracking()
            .Include(c => c.Funkos)
            .ToListAsync(ct);
    }

    /// <inheritdoc cref="ICategoryRepository.GetPaginatedCategoriesAsync(int, int, CancellationToken)"/>
    public Task<List<Category>> GetPaginatedCategoriesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        return _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    /// <inheritdoc cref="ICategoryRepository.DeleteCategoryAsync(int, CancellationToken)"/>
    public async Task<bool> DeleteCategoryAsync(
        int id,
        CancellationToken ct = default
    )
    {
        var response = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, ct);

        if (response is null)
            return false;

        response.IsDeleted = true;
        response.DeleteAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(ct);

        return true;
    }

    /// <inheritdoc cref="ICategoryRepository.RestoreCategoryAsync(int, CancellationToken)"/>
    public async Task<bool> RestoreCategoryAsync(
        int id,
        CancellationToken ct = default
    )
    {
        var response = await _context.Categories
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted, ct);

        if (response is null)
            return false;

        response.IsDeleted = false;
        response.DeleteAt = default;

        await _context.SaveChangesAsync(ct);

        return true;
    }

    /// <inheritdoc cref="ICategoryRepository.GetCategoryByIdAsync(int, CancellationToken)"/>
    public Task<Category?> GetCategoryByIdAsync(
        int id,
        CancellationToken ct = default
    )
    {
        return _context.Categories
            .AsNoTracking()
            .Include(c => c.Funkos)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }
}