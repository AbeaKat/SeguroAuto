namespace Insurance.Api.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
