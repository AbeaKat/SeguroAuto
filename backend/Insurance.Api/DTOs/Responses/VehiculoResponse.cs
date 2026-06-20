namespace Insurance.Api.DTOs.Responses;

public sealed class VehiculoResponse
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public short Anio { get; set; }
    public decimal ValorComercial { get; set; }
}
