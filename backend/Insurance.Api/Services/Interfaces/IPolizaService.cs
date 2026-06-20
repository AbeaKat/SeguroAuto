using Insurance.Api.DTOs.Requests;
using Insurance.Api.DTOs.Responses;

namespace Insurance.Api.Services.Interfaces;

public interface IPolizaService
{
    Task<List<PolizaResumenResponse>> GetAllAsync();
    Task<PolizaDetalleResponse?> GetByIdAsync(int id);
    Task<PolizaDetalleResponse> EmitirAsync(EmitirPolizaRequest request);
}
