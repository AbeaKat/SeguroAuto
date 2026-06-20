using Insurance.Api.Entities;

namespace Insurance.Api.Repositories.Interfaces;

public interface ICoberturaRepository
{
    Task<List<Cobertura>> GetAllActiveAsync();
    Task<List<Cobertura>> GetByIdsAsync(IEnumerable<int> ids);
}
