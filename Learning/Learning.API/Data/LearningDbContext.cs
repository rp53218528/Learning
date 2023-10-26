
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Data
{
    public class LearningDbContext: DbContext
    {
        public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<walk> walks { get; set; }
        public DbSet<WalkDifficulty> walkDifficulty { get; set; }

    }
}
