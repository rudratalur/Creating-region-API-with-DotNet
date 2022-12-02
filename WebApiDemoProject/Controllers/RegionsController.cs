using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using WebApiDemoProject.Models.Domain;
using WebApiDemoProject.Models.DTO;
using WebApiDemoProject.Repositories.IRepository;

namespace WebApiDemoProject.Controllers
{
    [Route("api/Regions")]
    [ApiController]

    
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        //[HttpGet]
        //public IActionResult GetAllRegions()
        //{
            
        //    var regions = _regionRepository.GetAll(); //getting all regions from entity or domain
        //    var regionsDTO = new List<RegionsDto>(); //IEnumarable
        //    regions.ToList().ForEach(region =>
        //    {
        //        var regionDTO = new RegionsDto()//Empty regionDTO object
        //        {
        //            Id = region.Id,
        //            RegionCode = region.Code,
        //            AreaName = region.AreaName,
        //            Area = region.Area,
        //            Latitude = region.Latitude,
        //            Longitude = region.Longitude,
        //            Population = region.Population
        //        };
        //        regionsDTO.Add(regionDTO); //added the data to the empty regionsDTO List
        //    });            
        //    return Ok(regionsDTO);
        //}

        ////[HttpPost("GetRegionById")]
        ////public ActionResult<RegionsDto> GetRegionById(int regionId)
        ////{
        ////    var region = _regionRepository.GetById(regionId);
        ////    if (regionId == null || regionId == 0)
        ////        return NotFound();

        ////}

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetRegionAsync()
        {
            //var allRegions = await _regionRepository.GetAllAsync();
            //var regionsDTO = _mapper.Map<RegionsDto>(allRegions);
            //return Ok(regionsDTO);
          var allRegions = await _regionRepository.GetAllAsync();
           var RegionsDto = _mapper.Map<List<RegionsDto>>(allRegions);
            return Ok(RegionsDto);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetRegionByIdAsync")]
        public async Task<IActionResult> GetRegionByIdAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var region = await _regionRepository.GetByIdAsync(id);
            var regionDto = _mapper.Map<RegionsDto>(region);
            return Ok(regionDto);

        }
   
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //validate request
            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }
            //request DTO to entity
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Latitude = addRegionRequest.Latitude,
                Longitude = addRegionRequest.Longitude,
                Population = addRegionRequest.Population,
                AreaName = addRegionRequest.AreaName
            };


            //Pass details to the Repo
            region = await _regionRepository.AddRegionAsync(region);

            //Converst the domain back yo Dto
            var regionDto = _mapper.Map<RegionsDto>(region);

            return CreatedAtAction(nameof(GetRegionByIdAsync),new { Id = regionDto.Id }, regionDto);

        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRegionAsync(int id)
        {
            //get region from databasde
          var region =  await _regionRepository.DeleteRegionAsync(id);

            //check for if null or undefined
            if(region == null)
            {
                return NotFound();
            }

            //convert response back to dto 
            var regionDto = new RegionsDto()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                AreaName = region.AreaName,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population
            };
            //return the OK response
            return Ok(regionDto);
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRegionAsync (int id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                AreaName = updateRegionRequest.AreaName,
                Latitude = updateRegionRequest.Latitude,
                Longitude = updateRegionRequest.Longitude,
                Population = updateRegionRequest.Population
            };
            //update the region using repository
            region = await _regionRepository.UpdateRegionAsync(id, region);
            //if null then not found
            if (region == null)
            {
                return NotFound();
            }
            //convert domain back to DTO
            var regionDto = new RegionsDto
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population,
                AreaName = region.AreaName
            };
            //Return OK Response
            return Ok(regionDto);
        }

        #region Private methods

        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add Region data is mondatory");
                
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Code), $"{nameof(AddRegionRequest.Code)} cannot be null or empty or with white space");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.AreaName))
            {
                ModelState.AddModelError(nameof(AddRegionRequest.AreaName), $"{nameof(AddRegionRequest.AreaName)} cannot be null or empty or with white space");
            }
            if (addRegionRequest.Area < 0 )
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Area), $"{nameof(AddRegionRequest.Area)} cannot be null or equal to zero");
            }
            if (addRegionRequest.Latitude <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Latitude), $"{nameof(AddRegionRequest.Latitude)} cannot be less than zero");
            }
            if (addRegionRequest.Longitude == 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Longitude), $"{nameof(AddRegionRequest.Longitude)} cannot be null or negetive");
            }
            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(AddRegionRequest.Population), $"{nameof(AddRegionRequest.Population)} cannot be null or equal to zero");
            }
            if(ModelState.ErrorCount> 0)
            {
                return false;
            }

            return true;
        }


        #endregion

    }


}
