using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();



            /* regions.ToList().ForEach(region =>
             {
                 var regionDTO = new Models.DTOs.Region()
                 {
                     Id = region.Id,
                     Code = region.Code,
                     Name = region.Name,
                     Area = region.Area,
                     Lat = region.Lat,
                     Long = region.Long,
                     Population = region.Population
                 };

                 regionsDTO.Add(regionDTO);

             });*/

            var regionsDTO =mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsDTO);

        }
    }
}
