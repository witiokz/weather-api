using Domain;
using Domain.Dto;

namespace Services.Mappers
{
    public class TemperaratureMappers
    {
        public static TemperatureViewModel FromTemperatureDtoToTemperatureViewModel(TemperatureDto temperatureDto)
        {
            return new TemperatureViewModel
            {
                CityId = temperatureDto.CityId,
                Temperature = temperatureDto.Temperature
            };
        }

        public static TemperatureDto FromTemperatureViewModelToTemperatureDto(TemperatureViewModel temperatureViewModel)
        {
            return new TemperatureDto
            {
                CityId = temperatureViewModel.CityId,
                Temperature = temperatureViewModel.Temperature
            };
        }

        public static TemperatureDto FromCityToTemperatureDto(City city)
        {
            return new TemperatureDto
            {
                CityId = city.Id,
                Temperature = city.Temperature
            };
        }
    }
}
