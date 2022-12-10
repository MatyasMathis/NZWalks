using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper) {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWDsAsync()
        {
            var wds=await walkDifficultyRepository.GetAllWDsAsync();

            var wdsDTO=mapper.Map<List<Models.DTOs.WalkDifficulty>>(wds);

            return Ok(wdsDTO);
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateWDAsync(Models.DTOs.AddWalkDifficultyRequest wd)
        {
            var wdAdd = new Models.Domain.WalkDifficulty()
            {
                Id = new Guid(),
                Code = wd.Code
            };

            await walkDifficultyRepository.CreateWDAsync(wdAdd);

            var wdDTO=mapper.Map<Models.DTOs.WalkDifficulty>(wdAdd);
            return Ok(wdDTO);
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWDAsync([FromRoute]Guid id)
        {
            var wd=await walkDifficultyRepository.GetWDsAsync(id);
            if (wd == null)
            {
                return BadRequest();
            }
           var wdDTO= mapper.Map<Models.DTOs.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWDAsync(Guid id, Models.DTOs.AddWalkDifficultyRequest wdToUpdate)
        {
            var wd = new Models.Domain.WalkDifficulty()
            {
                Id = id,
                Code = wdToUpdate.Code

            };
            await walkDifficultyRepository.UpdateWDAsync(id, wd);

            var wdDTO = mapper.Map<Models.DTOs.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWDAsync([FromRoute]Guid id)
        {
            var toDelete = await walkDifficultyRepository.DeleteWDAsync(id);
            if(toDelete == null)
            {
                return BadRequest();
            }

            var toDeleteDTO = mapper.Map<Models.DTOs.WalkDifficulty>(toDelete);

            return Ok(toDeleteDTO);
        }
        
    }
}
