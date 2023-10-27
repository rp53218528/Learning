using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllAsync();
    }
}
