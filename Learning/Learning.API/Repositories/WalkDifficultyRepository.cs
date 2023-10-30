using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly LearningDbContext learningDbContext;

        public WalkDifficultyRepository(LearningDbContext learningDbContext)
        {
            this.learningDbContext = learningDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await learningDbContext.walkDifficulty.AddAsync(walkDifficulty);
            await learningDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await learningDbContext.walkDifficulty.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                learningDbContext.walkDifficulty.Remove(existingWalkDifficulty);
                await learningDbContext.SaveChangesAsync();
                return existingWalkDifficulty;  
            }
            return null;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await learningDbContext.walkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await learningDbContext.walkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await learningDbContext.walkDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await learningDbContext.SaveChangesAsync();
            return existingWalkDifficulty;

        }
    }
}
