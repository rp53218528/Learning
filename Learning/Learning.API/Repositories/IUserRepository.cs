using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenicateAsync(string username, string password);
    }
}
