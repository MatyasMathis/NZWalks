using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWDsAsync();
        Task<WalkDifficulty> GetWDsAsync(Guid id);
        Task<WalkDifficulty> CreateWDAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWDAsync(Guid id, WalkDifficulty wd);
        Task<WalkDifficulty> DeleteWDAsync(Guid id);
    }
}
