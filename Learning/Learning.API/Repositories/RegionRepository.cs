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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await learningDbContext.AddAsync(region);
            await learningDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await learningDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            //Delete the region 
            learningDbContext.Regions.Remove(region);
            await learningDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await learningDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await learningDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await learningDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await learningDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
