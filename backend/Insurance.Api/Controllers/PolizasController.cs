using Insurance.Api.Common;
using Insurance.Api.DTOs.Requests;
using Insurance.Api.DTOs.Responses;
using Insurance.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PolizasController : ControllerBase
{
    private readonly IPolizaService _polizaService;

    public PolizasController(IPolizaService polizaService)
    {
        _polizaService = polizaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<PolizaResumenResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var polizas = await _polizaService.GetAllAsync();

        return Ok(ApiResponse<List<PolizaResumenResponse>>.Ok(polizas));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<PolizaDetalleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PolizaDetalleResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var poliza = await _polizaService.GetByIdAsync(id);

        if (poliza is null)
        {
            return NotFound(ApiResponse<PolizaDetalleResponse>.Fail("Póliza no encontrada."));
        }

        return Ok(ApiResponse<PolizaDetalleResponse>.Ok(poliza));
    }

    [HttpPost("emitir")]
    [ProducesResponseType(typeof(ApiResponse<PolizaDetalleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<PolizaDetalleResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<PolizaDetalleResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Emitir([FromBody] EmitirPolizaRequest request)
    {
        try
        {
            var poliza = await _polizaService.EmitirAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = poliza.Id },
                ApiResponse<PolizaDetalleResponse>.Ok(poliza, "Póliza emitida correctamente."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<PolizaDetalleResponse>.Fail(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<PolizaDetalleResponse>.Fail(ex.Message));
        }
    }
}
