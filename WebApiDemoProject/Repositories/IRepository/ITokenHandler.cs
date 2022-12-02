using WebApiDemoProject.Models.Domain;

namespace WebApiDemoProject.Repositories.IRepository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
