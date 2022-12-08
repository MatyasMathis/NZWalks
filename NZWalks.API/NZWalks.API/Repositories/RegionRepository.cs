using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext) {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var regionToDelete = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionToDelete == null)
            {
                return null;
            }

            nZWalksDbContext.Remove(regionToDelete);
            await nZWalksDbContext.SaveChangesAsync();
            return regionToDelete;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid regionId)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == regionId);
           
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
           var regionExisting=await nZWalksDbContext.Regions.FirstOrDefaultAsync(x=>x.Id == Id);
            if(regionExisting == null)
            {
                return null;

            }
            regionExisting.Code= region.Code;
            regionExisting.Name= region.Name;
            regionExisting.Area= region.Area;
            regionExisting.Lat= region.Lat; 
            regionExisting.Long= region.Long;
            regionExisting.Population= region.Population;

           await nZWalksDbContext.SaveChangesAsync();
            return regionExisting;

        }
    }
}
