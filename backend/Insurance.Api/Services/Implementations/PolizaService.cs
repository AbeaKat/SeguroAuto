using Insurance.Api.DTOs.Requests;
using Insurance.Api.DTOs.Responses;
using Insurance.Api.Entities;
using Insurance.Api.Repositories.Interfaces;
using Insurance.Api.Services.Interfaces;

namespace Insurance.Api.Services.Implementations;

public sealed class PolizaService : IPolizaService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ICoberturaRepository _coberturaRepository;
    private readonly IPolizaRepository _polizaRepository;

    public PolizaService(
        IClienteRepository clienteRepository,
        ICoberturaRepository coberturaRepository,
        IPolizaRepository polizaRepository)
    {
        _clienteRepository = clienteRepository;
        _coberturaRepository = coberturaRepository;
        _polizaRepository = polizaRepository;
    }

    public async Task<List<PolizaResumenResponse>> GetAllAsync()
    {
        var polizas = await _polizaRepository.GetAllAsync();

        return polizas.Select(x => new PolizaResumenResponse
        {
            Id = x.Id,
            NumeroPoliza = x.NumeroPoliza,
            Cliente = x.Cliente.Nombre,
            Vehiculo = $"{x.Vehiculo.Marca} {x.Vehiculo.Modelo} - {x.Vehiculo.Placa}",
            FechaEmision = x.FechaEmision,
            SumaAsegurada = x.SumaAsegurada,
            PrimaTotal = x.PrimaTotal,
            Estado = x.Estado
        }).ToList();
    }

    public async Task<PolizaDetalleResponse?> GetByIdAsync(int id)
    {
        var poliza = await _polizaRepository.GetByIdAsync(id);

        return poliza is null ? null : MapDetalle(poliza);
    }

    public async Task<PolizaDetalleResponse> EmitirAsync(EmitirPolizaRequest request)
    {
        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);

        if (cliente is null)
        {
            throw new KeyNotFoundException("El cliente indicado no existe o no está activo.");
        }

        var coberturaIds = request.CoberturasIds.Distinct().ToList();

        if (coberturaIds.Count == 0)
        {
            throw new ArgumentException("Debe seleccionar al menos una cobertura.");
        }

        var coberturas = await _coberturaRepository.GetByIdsAsync(coberturaIds);

        if (coberturas.Count != coberturaIds.Count)
        {
            throw new ArgumentException("Una o más coberturas seleccionadas no existen o no están activas.");
        }

        var primaTotal = coberturas.Sum(x => x.MontoCobertura);

        var vehiculo = new Vehiculo
        {
            Placa = request.Vehiculo.Placa.Trim().ToUpperInvariant(),
            Marca = request.Vehiculo.Marca.Trim(),
            Modelo = request.Vehiculo.Modelo.Trim(),
            Anio = request.Vehiculo.Anio,
            ValorComercial = request.Vehiculo.ValorComercial,
            FechaCreacion = DateTime.Now,
            Activo = true
        };

        var poliza = new Poliza
        {
            NumeroPoliza = GeneratePolicyNumber(),
            ClienteId = cliente.Id,
            Vehiculo = vehiculo,
            FechaEmision = DateTime.Now,
            SumaAsegurada = vehiculo.ValorComercial,
            PrimaTotal = primaTotal,
            Estado = "Emitida",
            PolizaCoberturas = coberturas.Select(cobertura => new PolizaCobertura
            {
                CoberturaId = cobertura.Id,
                MontoAplicado = cobertura.MontoCobertura
            }).ToList()
        };

        var created = await _polizaRepository.AddAsync(poliza);
        var detalle = await _polizaRepository.GetByIdAsync(created.Id);

        return detalle is null
            ? throw new InvalidOperationException("No se pudo recuperar la póliza emitida.")
            : MapDetalle(detalle);
    }

    private static string GeneratePolicyNumber()
    {
        return $"AUTO-{DateTime.Now:yyyyMMddHHmmssfff}";
    }

    private static PolizaDetalleResponse MapDetalle(Poliza poliza)
    {
        return new PolizaDetalleResponse
        {
            Id = poliza.Id,
            NumeroPoliza = poliza.NumeroPoliza,
            FechaEmision = poliza.FechaEmision,
            SumaAsegurada = poliza.SumaAsegurada,
            PrimaTotal = poliza.PrimaTotal,
            Estado = poliza.Estado,
            Cliente = new ClienteResponse
            {
                Id = poliza.Cliente.Id,
                Nombre = poliza.Cliente.Nombre,
                Identificacion = poliza.Cliente.Identificacion,
                Correo = poliza.Cliente.Correo,
                Telefono = poliza.Cliente.Telefono
            },
            Vehiculo = new VehiculoResponse
            {
                Id = poliza.Vehiculo.Id,
                Placa = poliza.Vehiculo.Placa,
                Marca = poliza.Vehiculo.Marca,
                Modelo = poliza.Vehiculo.Modelo,
                Anio = poliza.Vehiculo.Anio,
                ValorComercial = poliza.Vehiculo.ValorComercial
            },
            Coberturas = poliza.PolizaCoberturas.Select(pc => new CoberturaPolizaResponse
            {
                Id = pc.Cobertura.Id,
                Nombre = pc.Cobertura.Nombre,
                MontoAplicado = pc.MontoAplicado
            }).ToList()
        };
    }
}
