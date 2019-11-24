using Domain;
using Domain.Dto;

namespace Services.Mappers
{
    public class CityMappers
    {
        public static CityDto FromCityToCityDto(City city)
        {
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Temperature = city.Temperature,
                Updated = city.Updated
            };
        }

        public static City FromCityDtoToCity(CityDto cityDto)
        {
            return new City
            {
                Id = cityDto.Id,
                Name = cityDto.Name
            };
        }

        public static CityDto FromCityViewModelToCityDto(CityViewModel cityViewModel)
        {
            return new CityDto
            {
                Id = cityViewModel.Id,
                Name = cityViewModel.Name
            };
        }

        public static CityViewModel FromCityDtoToCityViewModel(CityDto cityDto)
        {
            return new CityViewModel
            {
                Id = cityDto.Id,
                Name = cityDto.Name
            };
        }
    }
}
