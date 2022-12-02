using System.ComponentModel.DataAnnotations;

namespace WebApiDemoProject.Models.Domain
{
    public class Role
    {
        [Key]
        public int Id{ get; set; }
        public string RoleName{ get; set; }
        //navigation prp
        public List<User_Role> UserRole { get; set; }

    }
}
