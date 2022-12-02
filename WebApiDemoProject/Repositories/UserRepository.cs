using Microsoft.EntityFrameworkCore;
using WebApiDemoProject.Data;
using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Repositories
{
    
    public class UserRepository : IUserRepository
    {
        private readonly WalksDbContext _db;

        public UserRepository(WalksDbContext db)
        {
            _db = db;
        }
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var findUser = await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower()  && u.Password == password);
            if(findUser == null)
            {
                return null;
            }
            var userRoles = await _db.UserRoles.Where(x => x.UserId == findUser.Id).ToListAsync();
            if (userRoles.Any())
            {
                findUser.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                 var roleName = await _db.Roles.FirstOrDefaultAsync(x=>x.Id==userRole.RoleId);
                    if(roleName ==null){
                        findUser.Roles.Add(roleName.RoleName);
                    }
                }
            }
            findUser.Password= null;
            return findUser;
        }
    }
}
