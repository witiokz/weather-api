using Domain.Dto;
using Domain.Filtering;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Tests
{
    public class CityServiceFakeService : ICityService
    {
        private readonly IList<CityDto> _cities;

        public CityServiceFakeService()
        {
            _cities = StaticData.GetData();
        }

        public Task<IPagedList<CityDto>> GetAll(CityFilterModel cityFilterModel)
        {
            return Task.FromResult(new PagedList<CityDto>(_cities.AsEnumerable(), cityFilterModel.PageNumber, cityFilterModel.PageSize, _cities.Count) as IPagedList<CityDto>);
        }

        public Task<CityDto> GetById(int id)
        {
            return Task.FromResult(_cities.FirstOrDefault(a => a.Id == id));
        }

        public Task<CityDto> Create(CityDto newItem)
        {
            newItem.Id = _cities.Count + 1;
            _cities.Add(newItem);
            return Task.FromResult(newItem);
        }

        public Task Update(CityDto cityDto)
        {
            var existing = _cities.First(a => a.Id == cityDto.Id);
            existing.Name = cityDto.Name;
            existing.Temperature = cityDto.Temperature;

            return Task.FromResult(0);
        }

        public Task<CityDto> Delete(int id)
        {
            var existing = _cities.First(a => a.Id == id);
            _cities.Remove(existing);

            return Task.FromResult(existing);
        }
    }

}
