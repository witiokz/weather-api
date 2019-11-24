using Domain.Dto;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Tests
{
    public class TemperatureServiceFakeService : ITemperatureService
    {
        private readonly IList<CityDto> _cities;

        public TemperatureServiceFakeService()
        {
            _cities = StaticData.GetData();
        }

        public Task<TemperatureDto> GetTemperatureByCityId(int cityId)
        {
            var existing = _cities.First(a => a.Id == cityId);
            return Task.FromResult(new TemperatureDto { CityId = cityId, Temperature = existing.Temperature });
        }

        public Task<CityDto> Modify(TemperatureDto temperatureDto)
        {
            var existing = _cities.First(a => a.Id == temperatureDto.CityId);
            existing.Temperature = temperatureDto.Temperature;
            return Task.FromResult(existing);
        }
    }

}
