using Domain.Dto;
using Domain.Filtering;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherAPI.Helpers
{
    public class Query
    {
        private readonly ICityService _cityService;

        public Query(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<IEnumerable<CityDto>> GetCities(CityFilterModel cityFilterModel)
        {
            var data = await _cityService.GetAll(cityFilterModel);

            return data.Items;
        }
    }
}
