using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Collections;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository

    {
        public readonly NZWalksDbContext nZWalksDbContext;
        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            walk.Id = new Guid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            nZWalksDbContext.SaveChanges();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {

           var walkToDelete=await nZWalksDbContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);

            if(walkToDelete==null)
            {
                return null;
            }
            nZWalksDbContext.Remove(walkToDelete);
            await nZWalksDbContext.SaveChangesAsync() ;
            return walkToDelete;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await 
                nZWalksDbContext.Walks
                .Include(x=>x.WalkDifficulty).Include(x=>x.Region)
                .ToListAsync();

        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
             var walk=await nZWalksDbContext.Walks.Include(x=>x.Region).Include(x=>x.WalkDifficulty).FirstOrDefaultAsync(x=>x.Id==id);
            return walk;
            
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkToUpdate=await nZWalksDbContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);
            if(walkToUpdate==null)
            {
                return null;
            }
            walkToUpdate.Name= walk.Name;
            walkToUpdate.Lenght= walk.Lenght;
            walkToUpdate.WalkDifficultyId= walk.WalkDifficultyId;
            walkToUpdate.RegionId= walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();
            return walkToUpdate;
        }
    }
}
