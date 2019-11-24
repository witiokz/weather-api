using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.Dto;
using Domain.Filtering;
using Microsoft.Extensions.Primitives;
using Services.Mappers;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WeatherAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {

        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Authorize(Roles = UserService.ReadRoleName)]
        [HttpGet]
        public async Task<IEnumerable<CityViewModel>> Get([FromQuery]CityFilterModel cityFilterModel)
        {
            var data = await _cityService.GetAll(cityFilterModel);

            Response?.Headers?.Add("total-count", new StringValues(data.TotalCount.ToString()));

            return data.Items.Select(i => CityMappers.FromCityDtoToCityViewModel(i));
        }

        [Authorize(Roles = UserService.ReadRoleName)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CityViewModel>> Get(int id)
        {
            var cityDto = await _cityService.GetById(id);

            if (cityDto == null)
            {
                return NotFound();
            }

            return CityMappers.FromCityDtoToCityViewModel(cityDto);
        }

        [Authorize(Roles = UserService.WriteRoleName)]
        [HttpPost]
        public async Task<ActionResult<CityDto>> Post(CityViewModel cityViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("City name can not be empty");
            }

            var cityDto = CityMappers.FromCityViewModelToCityDto(cityViewModel);
            cityDto = await _cityService.Create(cityDto);
            cityViewModel = CityMappers.FromCityDtoToCityViewModel(cityDto);

            return CreatedAtAction("Get", new { id = cityDto.Id }, cityViewModel);
        }

        [Authorize(Roles = UserService.WriteRoleName)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CityViewModel>> Delete(int id)
        {
            var city = await _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }

            var cityDto =  await _cityService.Delete(id);
            return CityMappers.FromCityDtoToCityViewModel(cityDto);
        }
    }
}
