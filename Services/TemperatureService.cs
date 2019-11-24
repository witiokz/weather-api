using Data;
using Domain.Dto;
using Services.Mappers;
using System.Threading.Tasks;

namespace Services
{
    public class TemperatureService: ITemperatureService
    {
        private readonly ICityRepository _cityRepository;
        public TemperatureService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<TemperatureDto> GetTemperatureByCityId(int cityId)
        {
            var city = await _cityRepository.GetById(cityId);
            return TemperaratureMappers.FromCityToTemperatureDto(city);
        }

        public async Task<CityDto> Modify(TemperatureDto temperatureDto)
        {
            var city = await _cityRepository.GetById(temperatureDto.CityId);
            city.Temperature = temperatureDto.Temperature;
            await _cityRepository.Update(city);

            return CityMappers.FromCityToCityDto(city);
        }
    }
}
