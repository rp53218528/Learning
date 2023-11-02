using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public interface ITokenHandler
    {
        Task<String> CreateTokenAsync(User user);
    }
}
