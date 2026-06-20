namespace Insurance.Api.DTOs.Responses;

public sealed class CoberturaPolizaResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal MontoAplicado { get; set; }
}
