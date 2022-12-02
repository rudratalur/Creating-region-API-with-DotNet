using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Repositories
{
    public class UserStaticRepository : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            //new User()
            //{
            //    FistName = "User1",
            //    LastName = "User", EmilAddress="user1@user.com", Id=1, UserName="User1@user.com", Password="user1", Roles=new List<string>{"manager"}
            //},
            //new User()
            //{
            //    FistName = "User2",
            //    LastName = "User", EmilAddress="user2@user.com", Id=2, UserName="User2@user.com", Password="user2", Roles=new List<string>{"admin","manager"}
            //},

        };
public async Task<User> AuthenticateUserAsync(string username, string password)
        {
           var checkUser = users.Find(u => u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) && u.Password.Equals(password));
            //if(checkUser != null)
            //{
            //    return checkUser;///
            //}
            return checkUser;
        }
    }
}
