using Insurance.Api.Data;
using Insurance.Api.Entities;
using Insurance.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Repositories.Implementations;

public sealed class PolizaRepository : IPolizaRepository
{
    private readonly AppDbContext _context;

    public PolizaRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Poliza>> GetAllAsync()
    {
        return _context.Polizas
            .AsNoTracking()
            .Include(x => x.Cliente)
            .Include(x => x.Vehiculo)
            .OrderByDescending(x => x.FechaEmision)
            .ToListAsync();
    }

    public Task<Poliza?> GetByIdAsync(int id)
    {
        return _context.Polizas
            .AsNoTracking()
            .Include(x => x.Cliente)
            .Include(x => x.Vehiculo)
            .Include(x => x.PolizaCoberturas)
                .ThenInclude(x => x.Cobertura)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Poliza> AddAsync(Poliza poliza)
    {
        _context.Polizas.Add(poliza);
        await _context.SaveChangesAsync();
        return poliza;
    }
}
