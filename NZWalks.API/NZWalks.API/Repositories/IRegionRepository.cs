using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid regionId);

        Task<Region> AddRegionAsync(Region region);

        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
