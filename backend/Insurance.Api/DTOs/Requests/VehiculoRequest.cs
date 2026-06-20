using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs.Requests;

public sealed class VehiculoRequest
{
    [Required]
    [MaxLength(20)]
    public string Placa { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Marca { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Modelo { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public short Anio { get; set; }

    [Range(1, double.MaxValue)]
    public decimal ValorComercial { get; set; }
}
