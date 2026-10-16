public class Funko
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Franquicia { get; set; } = string.Empty;
    public int Numero { get; set; }
    public int categoriaId {get; set;}
    public Category Categoria { get; set; } = new();
    public bool Exclusivo { get; set; }
    public double Precio { get; set; }
    public bool IsDelete {get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeleteAT {get; set;}
}
