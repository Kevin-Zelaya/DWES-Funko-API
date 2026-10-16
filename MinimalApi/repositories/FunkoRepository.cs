using MinimalApi.models;

public class FunkoRepository
{
    
    
    public readonly IDictionary<int, Funko> _funkos;
    public int id;

    public FunkoRepository()
    {
        _funkos = new Dictionary<int, Funko>();
        id = 1;

        CreateFunko(
        new FunkoDto(1, "Spider-Man", "Marvel", 1089, "Super Heroes", false, 14.99));
        CreateFunko(
        new FunkoDto(2, "Goku Ultra Instinct", "Dragon Ball Super", 860, "Animation", true, 25.50));
        CreateFunko(
        new FunkoDto(3, "Darth Vader", "Star Wars", 597, "Sci-Fi", false, 15.00));
        CreateFunko(
        new FunkoDto(4, "Harry Potter with Hedwig", "Harry Potter", 31, "Movies", false, 13.95));
        CreateFunko(
        new FunkoDto(5, "Batman (Black Light)", "DC Comics", 369, "Heroes", true, 35.00));

        

    }
    /// <summary>
    /// Crear un funko
    /// </summary>
    /// <param name="funko"></param>
    /// <returns></returns>
    public Funko CreateFunko(
        FunkoDto funko
    )
    {
        var nw = new Funko(
            id,
            funko.Nombre,
            funko.Franquicia,
            funko.Numero,
            funko.categoriam,
            funko.exclusivom,
            funko.precio

        );
        _funkos.Add(id, nw);
        id++;
        return nw;
    }
    /// <summary>
    /// Actualizar un funko
    /// </summary>
    /// <param name="funko"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Funko UpdateFunko(
        FunkoDto funko,
        int id
    )
    {
        var up = new Funko(
            id,
            funko.Nombre,
            funko.Franquicia,
            funko.Numero,
            funko.categoriam,
            funko.exclusivom,
            funko.precio

        );
        if(_funkos.TryGetValue(id, out Funko value))
        {
            _funkos.Remove(id);
            _funkos.Add(id, up);
            
        }
        else
        {
            return null;
        }
        return up;
        
    }
    /// <summary>
    /// Obtener todos los funkos
    /// </summary>
    /// <returns></returns>
    public ICollection<Funko> GetAll()
    {
        return _funkos.Values;
    }
    public Funko GetById(
        int id
    )
    {
        if(_funkos.TryGetValue(id, out Funko value))
        {
            return value;
        }
        return null;
    }
    /// <summary>
    /// Elimianr un funko
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Funko DeleteFunko(
        int id
    )
    {
        if(_funkos.TryGetValue(id, out Funko value))
        {
            return _funkos.Remove(id) ? value : null;
        }
        return null;

    }
}