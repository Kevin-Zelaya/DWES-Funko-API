using CSharpFunctionalExtensions;

public class SupplierService(
    ISupplierRepository _repository,
    ILogger<SupplierService> _logger
) : ISupplierService, IScoped
{
    // =========================================================
    // READ
    // =========================================================

    /// <inheritdoc cref="ISupplierService.GetAllSuppliersAsync(CancellationToken)"/>
    public async Task<Result<List<SupplierDto>, DomainError>> GetAllSuppliersAsync(
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETSUPPLIERS] Recuperando todos los proveedores.");

        var response = await _repository.GetAllSuppliersAsync(ct);

        return Result.Success<List<SupplierDto>, DomainError>(
            response.Select(s => s.ToDto()).ToList())
            .Tap(s => _logger.LogInformation(
                "[SERVICE-GETSUPPLIERS] Se recuperaron {Count} proveedores.",
                s.Count));
    }

    /// <inheritdoc cref="ISupplierService.GetPaginatedSuppliersAsync(int, int, CancellationToken)"/>
    public async Task<Result<PaginatedResouce<SupplierDto>, DomainError>> GetPaginatedSuppliersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETPAGINATEDSUPPLIERS] Recuperando página {PageNumber} con tamaño {PageSize}.",
            pageNumber, pageSize);

        var response = await _repository.GetPaginatedSuppliersAsync(
            pageNumber, pageSize, ct);

        return Result.Success<PaginatedResouce<SupplierDto>, DomainError>(
            new PaginatedResouce<SupplierDto>(
                Items: response.items.Select(s => s.ToDto()).ToList(),
                Count: response.count,
                TotalPages: response.totalPages,
                HasPreviusPage: response.HasPreviousPage,
                HasNextPage: response.HasNextPage
            ))
            .Tap(s => _logger.LogInformation(
                "[SERVICE-GETPAGINATEDSUPPLIERS] Se recuperaron {Count} proveedores de la página {PageNumber} de {TotalPages}.",
                s.Items.Count, pageNumber, s.TotalPages));
    }

    /// <inheritdoc cref="ISupplierService.GetSupplierByIdAsync(string, CancellationToken)"/>
    public async Task<Result<SupplierDto, DomainError>> GetSupplierByIdAsync(
        string id,
        CancellationToken ct = default)
    {
        _logger.LogDebug(
            "[SERVICE-GETSUPPLIER] Buscando proveedor con ID {Id}.", id);

        var response = await _repository.GetSupplierByIdAsync(id, ct);

        return response is not null
            ? Result.Success<SupplierDto, DomainError>(response.ToDto())
                .Tap(s => _logger.LogInformation(
                    "[SERVICE-GETSUPPLIER] Proveedor con ID {Id} recuperado correctamente.",
                    id))
            : Result.Failure<SupplierDto, DomainError>(
                NotFoundError.FromResource("Supplier", id))
                .Tap(_ => _logger.LogWarning(
                    "[SERVICE-GETSUPPLIER] No se encontró el proveedor con ID {Id}.",
                    id));
    }
}