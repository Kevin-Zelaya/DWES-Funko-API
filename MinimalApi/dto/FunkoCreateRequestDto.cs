using System.ComponentModel.DataAnnotations;

public class FunkoCreateRequestDto
{
    [Required(ErrorMessage = "Nombre requerido")]
    [MinLength(2)]
    [MaxLength(100)]
    public string Nombre;
    [Required(ErrorMessage = "Nombre requerido")]
    [MinLength(1)]
    [MaxLength(100)]
    public string Franquicia;
    [Required]
    [Range(0, int.MaxValue)]
    public int Numero;
    [Required(ErrorMessage = "Categoria requerido")]
    [MinLength(1)]
    [MaxLength(100)]
    public string categoriam;
    public bool exclusivom;
    public double preci;
}