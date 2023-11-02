
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Data
{
    public class LearningDbContext : DbContext
    {
        public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
               .HasOne(x => x.User)
               .WithMany(y => y.UserRoles)
               .HasForeignKey(x => x.UserId);
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<walk> walks { get; set; }
        public DbSet<WalkDifficulty> walkDifficulty { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> user_Roles { get; set; }

    }
}
