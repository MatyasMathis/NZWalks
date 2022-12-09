using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Profiles;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
       private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalkController(IWalkRepository walkRepository, IMapper mapper) {
        
            this.walkRepository = walkRepository;
            this.mapper = mapper;
       }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await walkRepository.GetAllWalksAsync();



            var walksDTO = mapper.Map<List<Models.DTOs.Walk>>(walks);

            return Ok(walksDTO);

        }


        [HttpPost]

        public async Task<IActionResult> AddWalkAsync(Models.DTOs.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Lenght = addWalkRequest.Lenght,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            await walkRepository.CreateWalkAsync(walk);

            var walkDTO = new Models.DTOs.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Lenght = walk.Lenght,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId


            };
            return Ok(walkDTO);



        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkAsync([FromRoute]Guid id)
        {
            var walk=await walkRepository.GetWalkAsync(id);
            if(walk == null)
            {
                return BadRequest();
            }
            var walkDTO=mapper.Map<Models.DTOs.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
           var toDelete= await walkRepository.DeleteWalkAsync(id);

            var toDeleteDTO = mapper.Map<Models.DTOs.Walk>(toDelete);
            return Ok(toDeleteDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id,[FromBody]Models.DTOs.UpdateWalkRequest walkUpdate)
        {
            var walkToUpdate = new Models.Domain.Walk()
            {
                Name= walkUpdate.Name,
                Lenght= walkUpdate.Lenght,
                WalkDifficultyId= walkUpdate.WalkDifficultyId,
                RegionId= walkUpdate.RegionId
            };
            if (walkToUpdate == null)
            {
                return BadRequest();
            }

            await walkRepository.UpdateWalkAsync(id, walkToUpdate);

            var walkDTO = mapper.Map<Models.DTOs.Walk>(walkToUpdate);
            return Ok(walkDTO);
        }


    }
}
