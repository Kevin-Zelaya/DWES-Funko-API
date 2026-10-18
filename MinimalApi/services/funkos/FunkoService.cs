
using CSharpFunctionalExtensions;

public class FunkoService(
    IFunkoRepository _repository,
    ILogger<FunkoService> _logger
) : IFunkoService, IScoped
{
    // =========================================================
    // CREATE
    // =========================================================

    /// <inheritdoc cref="IFunkoService.CreateFunkoAsync(FunkoCreateRequestDto, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> CreateFunkoAsync(
        FunkoCreateRequestDto funko,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-CREATE] Intentando crear un Funko.");
        // Comprobar si existe el proveedor
        if(!await _repository.ExistSupplierByIdAsync(funko.SupplierId, ct))
            return Result.Failure<FunkoDto, DomainError>(
                NotFoundError.FromResource("Supplier", funko.SupplierId));

        var response = await _repository.CreateFunkoAsync(
            funko.ToModel(), ct);

        return Result.Success<FunkoDto, DomainError>(response.ToDto())
            .Tap(f => _logger.LogInformation(
                "[SERVICE-CREATE] Funko creado correctamente con ID {Id}.",
                f.Id));
    }

    // =========================================================
    // READ
    // =========================================================

    /// <inheritdoc cref="IFunkoService.GetFunkoByIdAsync(int, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> GetFunkoByIdAsync(
        int id,
        CancellationToken ct)
    {
        _logger.LogDebug(
            "[SERVICE-GET] Buscando Funko con ID {Id}.", id);

        var response = await _repository.GetFunkoByIdAsync(id, ct);

        return response is not null
            ? Result.Success<FunkoDto, DomainError>(response.ToDto())
                .Tap(f => _logger.LogInformation(
                    "[SERVICE-GET] Funko con ID {Id} recuperado correctamente.",
                    id))
            : Result.Failure<FunkoDto, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-GET] No se encontró el Funko con ID {Id}.",
                    id));
    }

    /// <inheritdoc cref="IFunkoService.GetAllFunkos(CancellationToken)"/>
    public async Task<Result<List<FunkoDto>, DomainError>> GetAllFunkos(
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETALL] Recuperando todos los Funkos.");

        var response = await _repository.GetAllFunkosAsync(ct);

        return Result.Success<List<FunkoDto>, DomainError>(
            response.Select(f => f.ToDto()).ToList())
            .Tap(_ => _logger.LogInformation(
                "[SERVICE-GETALL] Se recuperaron {Count} Funkos.",
                response.Count));
    }

    /// <inheritdoc cref="IFunkoService.GetPaginatedFunkos(int, int, CancellationToken)"/>
    public async Task<Result<PaginatedResouce<FunkoDto>, DomainError>> GetPaginatedFunkos(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETPAGINATED] Recuperando página {PageNumber} con tamaño {PageSize}.",
            pageNumber, pageSize);

        var response = await _repository.GetPaginatedFunkosAsync(
            pageNumber, pageSize, ct);

        return Result.Success<PaginatedResouce<FunkoDto>, DomainError>(
            new(
                response.items.Select(f => f.ToDto()).ToList(),
                response.count,
                response.totalPages,
                response.HasPreviousPage,
                response.HasNextPage
            ))
            .Tap(_ => _logger.LogInformation(
                "[SERVICE-GETPAGINATED] Se recuperaron {Count} Funkos de la página {PageNumber}.",
                response.items.Count, pageNumber));
    }

    // =========================================================
    // UPDATE
    // =========================================================

    /// <inheritdoc cref="IFunkoService.UpdateFunkoAsync(int, FunkoUpdateRequestDto, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> UpdateFunkoAsync(
        int id,
        FunkoUpdateRequestDto request,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-UPDATE] Intentando actualizar el Funko con ID {Id}.",
            id);

        var response = await _repository.UpdateFunkoAsync(id, request, ct);

        return response is not null
            ? Result.Success<FunkoDto, DomainError>(response.ToDto())
                .Tap(f => _logger.LogInformation(
                    "[SERVICE-UPDATE] Funko con ID {Id} actualizado correctamente.",
                    f.Id))
            : Result.Failure<FunkoDto, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-UPDATE] No se encontró el Funko con ID {Id}.",
                    id));
    }
    /// <inheritdoc cref="IFunkoService.UpdateCategoryFromAsync(int, CategoryUpdateRequestDto, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> UpdateCategoryFromAsync(
        int id,
        CategoryUpdateRequestDto request,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-UPDATECATEGORY] Intentando actualizar la categoría del Funko con el ID {Id}.",
            id);

        var response = await _repository.UpdateCategoryFromFunkoAsync(id, request, ct);

        return response is not null
            ? Result.Success<FunkoDto, DomainError>(response.ToDto())
                .Tap(f => _logger.LogInformation(
                    "[SERVICE-UPDATECATEGORY] Funko con ID {Id} actualizado correctamente.",
                    f.Id))
            : Result.Failure<FunkoDto, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-UPDATECATEGORY] No se encontró el Funko con ID {Id}.",
                    id));
    }
    /// <inheritdoc cref="IFunkoService.UpdateSupplierFromFunko(int, int, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> UpdateSupplierFromFunko(
        int id,
        int supplierId,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-UPDATECATEGORY] Intentando actualizar la categoría del Funko con el ID {Id}.",
            id);
        // Verificar si existe el proveedor
        if(!await _repository.ExistSupplierByIdAsync(supplierId, ct))
            return Result.Failure<FunkoDto, DomainError>(
                NotFoundError.FromResource("Supplier", supplierId));
        var response = await _repository.UpdateSuplierFromFunkoAsync(id, supplierId, ct);

        return response is not null
            ? Result.Success<FunkoDto, DomainError>(response.ToDto())
                .Tap(f => _logger.LogInformation(
                    "[SERVICE-UPDATESUPPLIER] Funko con ID {Id} actualizado correctamente.",
                    f.Id))
            : Result.Failure<FunkoDto, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-UPDATESUPPLIER] No se encontró el Funko con ID {Id}.",
                    id));
    }



    // =========================================================
    // DELETE / RESTORE
    // =========================================================

    /// <inheritdoc cref="IFunkoService.DeleteFunkoAsync(int, CancellationToken)"/>
    public async Task<Result<bool, DomainError>> DeleteFunkoAsync(
        int id,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-DELETE] Intentando eliminar el Funko con ID {Id}.",
            id);

        var response = await _repository.DeleteFunkoAsync(id, ct);

        return response
            ? Result.Success<bool, DomainError>(true)
                .Tap(_ => _logger.LogInformation(
                    "[SERVICE-DELETE] Funko con ID {Id} eliminado correctamente.",
                    id))
            : Result.Failure<bool, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-DELETE] No se encontró el Funko con ID {Id}.",
                    id));
    }

    /// <inheritdoc cref="IFunkoService.RestoreDeletedFunkoAsync(int, CancellationToken)"/>
    public async Task<Result<FunkoDto, DomainError>> RestoreDeletedFunkoAsync(
        int id,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-RESTORE] Intentando restaurar el Funko con ID {Id}.",
            id);

        var response = await _repository.RestoreFunkoAsync(id, ct);

        return response is not null
            ? Result.Success<FunkoDto, DomainError>(response.ToDto())
                .Tap(f => _logger.LogInformation(
                    "[SERVICE-RESTORE] Funko con ID {Id} restaurado correctamente.",
                    f.Id))
            : Result.Failure<FunkoDto, DomainError>(
                FunkoError.NotFound(id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-RESTORE] No se encontró un Funko eliminado con ID {Id}.",
                    id));
    }

    // =========================================================
    // CATEGORIES
    // =========================================================

    /// <inheritdoc cref="IFunkoService.GetAllCategories(CancellationToken)"/>
    public async Task<Result<List<Category>, DomainError>> GetAllCategories(
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETCATEGORIES] Recuperando todas las categorías.");

        var response = await _repository.GetAllCategoriesAsync(ct);

        return Result.Success<List<Category>, DomainError>(response)
            .Tap(_ => _logger.LogInformation(
                "[SERVICE-GETCATEGORIES] Se recuperaron {Count} categorías.",
                response.Count));
    }
}
