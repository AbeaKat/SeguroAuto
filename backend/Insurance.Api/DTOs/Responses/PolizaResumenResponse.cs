namespace Insurance.Api.DTOs.Responses;

public sealed class PolizaResumenResponse
{
    public int Id { get; set; }
    public string NumeroPoliza { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public string Vehiculo { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    public decimal SumaAsegurada { get; set; }
    public decimal PrimaTotal { get; set; }
    public string Estado { get; set; } = string.Empty;
}
