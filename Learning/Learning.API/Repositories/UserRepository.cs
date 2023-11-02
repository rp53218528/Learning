using Learning.API.Data;
using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LearningDbContext learningDbContext;

        public UserRepository(LearningDbContext learningDbContext)
        {
            this.learningDbContext = learningDbContext;
        }
        public async Task<User> AuthenicateAsync(string username, string password)
        {
            var user = await learningDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()
            && x.Password == password);
            if(user == null)
            {
                return null;
            }
            var userRoles = await learningDbContext.user_Roles.Where(x=>x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await learningDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if(role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
