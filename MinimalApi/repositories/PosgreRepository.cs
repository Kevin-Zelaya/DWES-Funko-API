using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Repositorio para conexión con el contexto por parte de los funkos
/// </summary>
/// <param name="_context">Contexto configurado con conexión a postgres</param>
public class PosgreRepository(
    AppDbContext _context
) : IRepository
{
    ///<inheritdoc cref="IRepository.CreateFunkoAsync(Funko, CancellationToken)"/>
    public async Task<Funko> CreateFunkoAsync(
        Funko funko,
        CancellationToken ct = default
    )
    {
        _context.Funkos.Add(funko);
        await _context.SaveChangesAsync(ct);
        return funko;
    }
    ///<inheritdoc cref="IRepository.UpdateFunkoAsync(int, Funko, CancellationToken)"/>
    public async Task<Funko> UpdateFunkoAsync(
        int id,
        Funko funko,
        CancellationToken ct = default
    )
    {
        var response = await _context.Funkos.FirstOrDefaultAsync(u => u.Id == id);
        // Reasignamos valores
        response.Nombre = funko.Nombre;
        response.Numero = funko.Numero;
        response.categoriaId = funko.categoriaId;
        response.Precio = funko.Precio;
        response.Exclusivo = funko.Exclusivo;
        response.UpdatedAt = DateTime.Now;
        response.Franquicia = funko.Franquicia;
        await _context.SaveChangesAsync(ct);
        return response;
    }
    ///<inheritdoc cref="IRepository.GetAllFunkosAsync(CancellationToken)"/>
    public Task<List<Funko>> GetAllFunkosAsync(
        CancellationToken ct
    )
    {
        return _context.Funkos
            .AsNoTracking()
            .Include(u => u.Categoria)
            .ToListAsync(ct);
    }
    ///<inheritdoc cref="IRepository.GetPaginatedFunkosAsync(int, int, CancellationToken)"/>
    public Task<List<Funko>> GetPaginatedFunkosAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct
    )
    {
        return _context.Funkos
            .AsNoTracking()
            .Include(u => u.Categoria)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }
    ///<inheritdoc cref="IRepository.DeleteFunkoAsync(int, CancellationToken)"/>
    public async Task<bool> DeleteFunkoAsync(
        int id,
        CancellationToken ct
    )
    {
        var response = await _context.Funkos
            .FirstOrDefaultAsync(u => u.Id == id, ct);
        if (response is null) return false;
        response.IsDelete = true;
        response.DeleteAT = DateTime.Now;
        await _context.SaveChangesAsync(ct);
        return true;
    }
    ///<inheritdoc cref="IRepository.RestoreFunkoAsync(int, CancellationToken)"/>
    public async Task<bool> RestoreFunkoAsync(
        int id,
        CancellationToken ct
    )
    {
        var response = await _context.Funkos
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == id && u.IsDelete, ct);
        if (response is null) return false;
        response.IsDelete = false;
        await _context.SaveChangesAsync(ct);
        return true;
    }
    ///<inheritdoc cref="IRepository.GetFunkoByIdAsync(int, CancellationToken)"/>
    public Task<Funko?> GetFunkoByIdAsync(
        int id,
        CancellationToken ct
    )
    {
        return _context.Funkos
            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }
}