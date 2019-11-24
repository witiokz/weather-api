using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Services.Mappers;

namespace WeatherAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {

        private readonly ICityService _cityService;
        private readonly ITemperatureService _temperatureService;

        public TemperaturesController(ICityService cityService, ITemperatureService temperatureService)
        {
            _cityService = cityService;
            _temperatureService = temperatureService;
        }

        [Authorize(Roles = UserService.ReadRoleName)]
        [HttpGet("{cityId}")]
        public async Task<ActionResult<TemperatureViewModel>> Get(int cityId)
        {
            var temperatureDto = await _temperatureService.GetTemperatureByCityId(cityId);
            var temperatureViewModel = TemperaratureMappers.FromTemperatureDtoToTemperatureViewModel(temperatureDto);

            if (temperatureDto == null)
            {
                return NotFound();
            }

            return temperatureViewModel;
        }

        [Authorize(Roles = UserService.WriteRoleName)]
        [HttpPost("{cityId}")]
        public async Task<ActionResult<CityDto>> Post(int cityId, TemperatureDto temperatureDto)
        {
            if (cityId != temperatureDto.CityId)
            {
                return BadRequest();
            }

            var cityDto = await _temperatureService.Modify(temperatureDto);

            return CreatedAtAction("Get", new { cityId = cityDto.Id }, cityDto);
        }

        [Authorize(Roles = UserService.WriteRoleName)]
        [HttpPut("{cityId}")]
        public async Task<IActionResult> Put(int cityId, TemperatureDto temperatureDto)
        {
            if (cityId != temperatureDto.CityId)
            {
                return BadRequest();
            }

            try
            {
                await _temperatureService.Modify(temperatureDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_cityService.GetById(cityId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
