using Insurance.Api.Common;
using Insurance.Api.DTOs.Responses;
using Insurance.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoberturasController : ControllerBase
{
    private readonly ICoberturaRepository _coberturaRepository;

    public CoberturasController(ICoberturaRepository coberturaRepository)
    {
        _coberturaRepository = coberturaRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<CoberturaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var coberturas = await _coberturaRepository.GetAllActiveAsync();

        var response = coberturas.Select(x => new CoberturaResponse
        {
            Id = x.Id,
            Nombre = x.Nombre,
            Descripcion = x.Descripcion,
            MontoCobertura = x.MontoCobertura
        }).ToList();

        return Ok(ApiResponse<List<CoberturaResponse>>.Ok(response));
    }
}
