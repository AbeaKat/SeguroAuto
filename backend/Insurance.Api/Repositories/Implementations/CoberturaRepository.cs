using Insurance.Api.Data;
using Insurance.Api.Entities;
using Insurance.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Repositories.Implementations;

public sealed class CoberturaRepository : ICoberturaRepository
{
    private readonly AppDbContext _context;

    public CoberturaRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Cobertura>> GetAllActiveAsync()
    {
        return _context.Coberturas
            .AsNoTracking()
            .Where(x => x.Activo)
            .OrderBy(x => x.Nombre)
            .ToListAsync();
    }

    public Task<List<Cobertura>> GetByIdsAsync(IEnumerable<int> ids)
    {
        var idsList = ids.Distinct().ToList();

        return _context.Coberturas
            .AsNoTracking()
            .Where(x => x.Activo && idsList.Contains(x.Id))
            .ToListAsync();
    }
}
