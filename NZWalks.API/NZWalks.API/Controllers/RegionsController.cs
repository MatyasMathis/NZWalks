using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    //[Route("Regions")]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

           

            var regionsDTO = mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsDTO);

        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTOs.AddRegionRequest addRegionRequest)
        {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };


            //Pass deatails to Repository

            await regionRepository.AddRegionAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTOs.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var regionToDelete = await regionRepository.DeleteAsync(id);


            //If null NotFound
            if (regionToDelete == null)
            {
                return NotFound();
            }

            //Convert response back to DTO

            var regionDTO = new Models.DTOs.Region
            {
                Code = regionToDelete.Code,
                Area = regionToDelete.Area,
                Lat = regionToDelete.Lat,
                Long = regionToDelete.Long,
                Name = regionToDelete.Name,
                Population = regionToDelete.Population
            };

            //Return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTOs.UpdateRegionRequest updateRegionRequest) {
            //Convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };
            //Update Region using repository

            region=await regionRepository.UpdateAsync(id, region);

            //IFNULL NotFound
            if(region== null) { return NotFound(); }
            //Convert Domain back to DTO
            var regionDTO = new Models.DTOs.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            //Return OK response

            return Ok(regionDTO);
        }
    }
}
