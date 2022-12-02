using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDemoProject.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmilAddress { get; set; }
        public string Password { get; set; }
       // [NotMapped]
        public List<string> Roles { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        //Navigation Property

        public List<User_Role> UserRole { get; set; }
    }
}
