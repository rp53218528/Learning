using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly LearningDbContext learningDbContext;
        public RegionRepository(LearningDbContext learningDbContext)
        {
                this.learningDbContext = learningDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await learningDbContext.Regions.ToListAsync();
        }
    }
}
