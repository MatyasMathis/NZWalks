using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<WalkDifficulty> CreateWDAsync(WalkDifficulty walkDifficulty)
        {
            await nZWalksDbContext.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWDAsync(Guid id)
        {
            var toDelete =await nZWalksDbContext.WalksDifficulty.FirstOrDefaultAsync(x=>x.Id==id);
            if (toDelete == null)
            {
                return null;
            }
            nZWalksDbContext.WalksDifficulty.Remove(toDelete);
            await nZWalksDbContext.SaveChangesAsync();

            return toDelete;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWDsAsync()
        {
            return await nZWalksDbContext.WalksDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWDsAsync(Guid id)
        {
            return  await nZWalksDbContext.WalksDifficulty.FirstOrDefaultAsync(x => x.Id == id);
           
           
        }

        public async Task<WalkDifficulty> UpdateWDAsync(Guid id, WalkDifficulty wd)
        {
            var wdToUpdate = await nZWalksDbContext.WalksDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(wdToUpdate == null) {
                return null;
            }
            wdToUpdate.Code = wd.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return wdToUpdate;
        }
    }
}
