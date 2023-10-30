using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<walk>> GetAllAsync();
        Task<walk> GetAsync(Guid id);
        Task<walk> AddAsync(walk walk);
        Task<walk> UpdateAsync(Guid id, walk walk);
        Task<walk> DeleteAsync(Guid id);
    }
}
