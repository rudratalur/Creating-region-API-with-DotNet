using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiDemoProject.Data;
using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _walksDbContext;
        private readonly IMapper _mapper;

        public RegionRepository(WalksDbContext walksDbContext, IMapper mapper)

        {
            _walksDbContext = walksDbContext;
            _mapper = mapper;
        }

        public void AddRegion()
        {
            throw new NotImplementedException();
        }


        public void DeleteRegion(Region region)
        {
            throw new NotImplementedException();
        }



        //public RegionRepository(WalksDbContext walksDbContext)
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            var allRegions = await _walksDbContext.Regions.ToListAsync();
            return allRegions;
        }

        //public Region GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Region> GetByIdAsync(int id)
        {
            return await _walksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await _walksDbContext.Regions.AddAsync(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        }
        public void UpdateRegion(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region> AddRegionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Region> DeleteRegionAsync(int id)
        {
            var region = await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            _walksDbContext.Regions.Remove(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        }


        public async Task<Region> UpdateRegionAsync(int id, Region region)
        {
           var Existingregion = await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(Existingregion == null)
            {
                return null;
            }
            Existingregion.Code=region.Code;
            Existingregion.Area=region.Area;
            Existingregion.AreaName = region.AreaName;
            Existingregion.Latitude=region.Latitude;
            Existingregion.Longitude = region.Longitude;
            Existingregion.Population=region.Population;
            await _walksDbContext.SaveChangesAsync();
            return Existingregion;
        }
    }
}
