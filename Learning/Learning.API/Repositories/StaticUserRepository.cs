using Learning.API.Models.Domain;
using System.Linq.Expressions;

namespace Learning.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Read Only",
            //    LastName = "User",
            //    EmailAddress = "readonly@user.com",
            //    Id = Guid.NewGuid(),
            //    Username = "readonly@user.com",
            //    Password = "Readonly@user",
            //    Roles = new List<string> {"reader"}
            //},
            //new User()
            //{
            //    FirstName = "Read Write",
            //    LastName = "User",
            //    EmailAddress = "readwrite@user.com",
            //    Id = Guid.NewGuid(),
            //    Username = "readwrite@user.com",
            //    Password = "Readwrite@user",
            //    Roles = new List<string> {"reader","writer"}
            //}
        };

        public async Task<User> AuthenicateAsync(string username, string password)
        {
            var user = users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
            
            return user;
        }
    }
}
