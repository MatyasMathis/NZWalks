using NZWalks.API.Models.Domain;
using System.Collections;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
         Task<IEnumerable<Walk>> GetAllWalksAsync();

         Task<Walk> GetWalkAsync(Guid id);

         Task<Walk> CreateWalkAsync(Walk walk);

         Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

         Task<Walk> DeleteWalkAsync(Guid id);

    }
}
