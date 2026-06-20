using Insurance.Api.Data;
using Insurance.Api.Entities;
using Insurance.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Repositories.Implementations;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Cliente>> GetAllActiveAsync()
    {
        return _context.Clientes
            .AsNoTracking()
            .Where(x => x.Activo)
            .OrderBy(x => x.Nombre)
            .ToListAsync();
    }

    public Task<Cliente?> GetByIdAsync(int id)
    {
        return _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);
    }
}
