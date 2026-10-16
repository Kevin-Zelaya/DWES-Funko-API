public interface IRepository
{
    /// <summary>
    /// Crear un funko de forma asincrona
    /// </summary>
    /// <param name="funko">Modelo funko con propiedades a crear</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>Funko creado</returns>
    Task<Funko> CreateFunkoAsync(Funko funko, CancellationToken ct = default);
    /// <summary>
    /// Eliminar de forma lógica un funko
    /// se le agrega el tiempo de eliminación
    /// </summary>
    /// <param name="id">Id del funkko a eliminar</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>True o false dependiendo de si se encontró o no</returns>
    Task<bool> DeleteFunkoAsync(int id, CancellationToken ct);
    /// <summary>
    /// Obtener todos los funkos, incluyen su categoria
    /// no página
    /// </summary>
    /// <param name="ct">token de cancelación</param>
    /// <returns> Lista sin filtrar de funkos </returns>
    Task<List<Funko>> GetAllFunkosAsync(CancellationToken ct);
    /// <summary>
    /// Obtener un funko por id
    /// </summary>
    /// <param name="id">id del usuario a buscar</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>Funko encontrado o null</returns>
    Task<Funko?> GetFunkoByIdAsync(int id, CancellationToken ct);
    /// <summary>
    /// Obtener lista páginada de funkos
    /// </summary>
    /// <param name="pageNumber">Número de página</param>
    /// <param name="pageSize">Tamaño de página</param>
    /// <param name="ct">token de cancelación</param>
    /// <returns>Lista paginada de usuarios</returns>
    Task<List<Funko>> GetPaginatedFunkosAsync(int pageNumber, int pageSize, CancellationToken ct);
    /// <summary>
    /// Restablecer funko eliminado de forma lógica
    /// </summary>
    /// <param name="id">Id del funko a restablecer</param>
    /// <param name="ct">Token de cancelación</param>
    /// <returns>True o false dependiendo de si se encontró</returns>
    Task<bool> RestoreFunkoAsync(int id, CancellationToken ct);
    /// <summary>
    /// Actualizar funko
    /// </summary>
    /// <param name="id">Id del funko a actualizar</param>
    /// <param name="funko">Data del funko</param>
    /// <param name="ct">Token a cancelación</param>
    /// <returns>Data actualiada</returns>
    Task<Funko> UpdateFunkoAsync(int id, Funko funko, CancellationToken ct = default);
}