namespace Insurance.Api.Entities;

public class Vehiculo
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public short Anio { get; set; }
    public decimal ValorComercial { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
