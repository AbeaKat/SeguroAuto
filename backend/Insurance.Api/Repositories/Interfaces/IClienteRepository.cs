using Insurance.Api.Entities;

namespace Insurance.Api.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllActiveAsync();
    Task<Cliente?> GetByIdAsync(int id);
}
