namespace Insurance.Api.DTOs.Responses;

public sealed class PolizaDetalleResponse
{
    public int Id { get; set; }
    public string NumeroPoliza { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
    public decimal SumaAsegurada { get; set; }
    public decimal PrimaTotal { get; set; }
    public string Estado { get; set; } = string.Empty;
    public ClienteResponse Cliente { get; set; } = new();
    public VehiculoResponse Vehiculo { get; set; } = new();
    public List<CoberturaPolizaResponse> Coberturas { get; set; } = new();
}
