using MinimalApi.models;

public static class FunkoMapper
{
    
    public static FunkoDto ToDto(this Funko funko)
    {
        return new FunkoDto(
            funko.Id,
            funko.Nombre,
            funko.Franquicia,
            funko.Numero,
            funko.categoriam,
            funko.exclusivom,
            funko.precio
        );
    }

    public static Funko ToEntity(this FunkoDto dto)
    {
        return new Funko(
            dto.Id,
            dto.Nombre,
            dto.Franquicia,
            dto.Numero,
            dto.categoriam,
            dto.exclusivom,
            dto.precio
        );
    }
}