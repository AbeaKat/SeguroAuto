namespace Insurance.Api.DTOs.Responses;

public sealed class CoberturaResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal MontoCobertura { get; set; }
}
