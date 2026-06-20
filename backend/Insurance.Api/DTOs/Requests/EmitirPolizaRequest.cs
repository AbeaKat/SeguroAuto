using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs.Requests;

public sealed class EmitirPolizaRequest
{
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }

    [Required]
    public VehiculoRequest Vehiculo { get; set; } = new();

    [Required]
    [MinLength(1)]
    public List<int> CoberturasIds { get; set; } = new();
}
