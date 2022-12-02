using WebApiDemoProject.Models.Domain;

namespace WebApiDemoProject.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}
