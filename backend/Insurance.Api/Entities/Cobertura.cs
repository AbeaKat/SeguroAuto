namespace Insurance.Api.Entities;

public class Cobertura
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal MontoCobertura { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
}
