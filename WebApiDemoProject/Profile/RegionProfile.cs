using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Models.DTO;

namespace WebApiDemoProject.Profile
{

    public class RegionProfile : AutoMapper.Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionsDto>().ReverseMap();

        }
    }
}
