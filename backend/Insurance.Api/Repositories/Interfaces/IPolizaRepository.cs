using Insurance.Api.Entities;

namespace Insurance.Api.Repositories.Interfaces;

public interface IPolizaRepository
{
    Task<List<Poliza>> GetAllAsync();
    Task<Poliza?> GetByIdAsync(int id);
    Task<Poliza> AddAsync(Poliza poliza);
}
