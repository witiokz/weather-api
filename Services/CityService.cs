using Data;
using System.Threading.Tasks;
using System.Linq;
using Services.Mappers;
using Domain.Dto;
using Domain.Filtering;
using System;

namespace Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<IPagedList<CityDto>> GetAll(CityFilterModel cityFilterModel)
        {
            var items = await _cityRepository.GetAll(cityFilterModel);

            return new PagedList<CityDto>(
                items.Items.Select(i => CityMappers.FromCityToCityDto(i)).ToList(), 
                items.PageNumber,
                items.PageSize, 
                items.TotalCount);
        }

        public async Task<CityDto> GetById(int id)
        {
            var city = await _cityRepository.GetById(id);
            return CityMappers.FromCityToCityDto(city);
        }

        public async Task<CityDto> Create(CityDto cityDto)
        {
            var city = CityMappers.FromCityDtoToCity(cityDto);
            city.Updated = DateTime.UtcNow;
            await _cityRepository.Create(city);

            return CityMappers.FromCityToCityDto(city);
        }

        public async Task Update(CityDto cityDto)
        {
            var city = CityMappers.FromCityDtoToCity(cityDto);
            city.Updated = DateTime.UtcNow;
            await _cityRepository.Update(city);
        }

        public async Task<CityDto> Delete(int id)
        {
            var city = await _cityRepository.Delete(id);
            return CityMappers.FromCityToCityDto(city);
        }
    }
}
