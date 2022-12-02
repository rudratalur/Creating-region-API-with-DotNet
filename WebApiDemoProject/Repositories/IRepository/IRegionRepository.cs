using WebApiDemoProject.Models.Domain;

namespace WebApiDemoProject.Repositories.IRepository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(int id);
        Task<Region> AddRegionAsync();
        Task<Region> UpdateRegionAsync(int id, Region region);
        Task<Region> DeleteRegionAsync(int id);
        Task<Region> AddRegionAsync(Region region);
    }
}
