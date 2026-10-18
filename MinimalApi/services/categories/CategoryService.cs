using CSharpFunctionalExtensions;

public class CategoryService(
    ICategoryRepository _repository,
    ILogger<CategoryService> _logger
) : ICategoryService, IScoped
{
    /// <inheritdoc cref="ICategoryService.GetAllCategoriesAsync(CancellationToken)"/>
    public async Task<Result<List<CategoryDto>, DomainError>> GetAllCategoriesAsync(
        CancellationToken ct
    )
    {
        _logger.LogDebug("[CATEGORY-GETALL] Intentando obtener la lista completa de categorias.");
        var response = await _repository.GetAllCategoriesAsync(ct);
        return Result.Success<List<CategoryDto>, DomainError>(
            response.Select(f => f.ToDto()).ToList()
        )
        .Tap(c => _logger.LogInformation("[CATEGORY-GETALL] Retornando lista completa de categorias."));
    }
    /// <inheritdoc cref="ICategoryService.GetPaginatedCategoriesAsync(int, int, CancellationToken)"/>
    public async Task<Result<PaginatedResouce<CategoryDto>, DomainError>> GetPaginatedCategoriesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct
    )
    {
        _logger.LogDebug("[CATEGORY-GETPAGINATED] Intentando obtener la lista páginada de categorias.");
        var response = await _repository.GetPaginatedCategoriesAsync(pageNumber, pageSize, ct);
        return Result.Success<PaginatedResouce<CategoryDto>, DomainError>(
            new PaginatedResouce<CategoryDto>(
                response.items.Select(c => c.ToDto()).ToList(),
                response.count,
                response.totalPages,
                response.HasPreviousPage,
                response.HasNextPage

            )
        )
        .Tap(c => _logger.LogInformation("[CATEGORY-GETPAGINATED] Retornando lista páginada de categorias."));
    }
    /// <inheritdoc cref="ICategoryService.GetCategoryByNameAsync(string, CancellationToken)"/>
    public async Task<Result<CategoryDto?, DomainError>> GetCategoryByNameAsync(
        string name,
        CancellationToken ct
    )
    {
        _logger.LogDebug("[CATEGORY-GETBYNAME] Intentando obtener la lista páginada de categorias.");
        var response = await _repository.GetCategoryByName(name, ct);
        return response is not null
            ? Result.Success<CategoryDto?, DomainError>(
                response.ToDto())
                .Tap(c => _logger.LogInformation("[CATEGORY-GETPAGINATED] Retornando lista páginada de categorias."))
            : Result.Failure<CategoryDto?, DomainError>(
                NotFoundError.FromResource("Category", 0))
                .Tap(c => _logger.LogError("[CATEGORY-GETBYNAME] No se ha encontrado la categoria con el nombre {name}", name));
    }
}