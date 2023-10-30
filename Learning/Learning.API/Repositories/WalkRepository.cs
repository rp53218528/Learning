using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly LearningDbContext learningDbContext;

        public WalkRepository(LearningDbContext learningDbContext)
        {
            this.learningDbContext = learningDbContext;
        }

        public async Task<walk> AddAsync(walk walk)
        {
            walk.Id = Guid.NewGuid();
            await learningDbContext.walks.AddAsync(walk);
            await learningDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<walk> DeleteAsync(Guid id)
        {
            var existingWalk = await learningDbContext.walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            learningDbContext.walks.Remove(existingWalk);
            await learningDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<walk>> GetAllAsync()
        {
            var walks = await learningDbContext.walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
            return walks;
        }

        public Task<walk> GetAsync(Guid id)
        {
            var walks = learningDbContext.walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
            return walks;
        }

        public async Task<walk> UpdateAsync(Guid id, walk walk)
        {
            var existingWalk = await learningDbContext.walks.FindAsync(id);
            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await learningDbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }
    }
}
