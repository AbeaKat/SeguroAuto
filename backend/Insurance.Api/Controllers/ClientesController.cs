using Insurance.Api.Common;
using Insurance.Api.DTOs.Responses;
using Insurance.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClientesController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ClienteResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteRepository.GetAllActiveAsync();

        var response = clientes.Select(x => new ClienteResponse
        {
            Id = x.Id,
            Nombre = x.Nombre,
            Identificacion = x.Identificacion,
            Correo = x.Correo,
            Telefono = x.Telefono
        }).ToList();

        return Ok(ApiResponse<List<ClienteResponse>>.Ok(response));
    }
}
