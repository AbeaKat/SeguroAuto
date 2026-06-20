namespace Insurance.Api.Entities;

public class Poliza
{
    public int Id { get; set; }
    public string NumeroPoliza { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal SumaAsegurada { get; set; }
    public decimal PrimaTotal { get; set; }
    public string Estado { get; set; } = "Emitida";

    public Cliente Cliente { get; set; } = null!;
    public Vehiculo Vehiculo { get; set; } = null!;
    public ICollection<PolizaCobertura> PolizaCoberturas { get; set; } = new List<PolizaCobertura>();
}
