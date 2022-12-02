using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiDemoProject.Models.Domain
{
    public class Walk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WalkId { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        [ForeignKey("Id")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
