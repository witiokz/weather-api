using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Filtering.FilterExtensions
{
    public static class Ext
    {
        public static IQueryable<City> ApplyFilter(this IQueryable<City> items, CityFilterModel cityFilterModel)
        {
            if (!string.IsNullOrEmpty(cityFilterModel.Name))
            {
                items = items.Where(i => i.Name == cityFilterModel.Name.ToLower());
            }

            if (cityFilterModel.Temperature.HasValue)
            {
                items = items.Where(i => i.Temperature == cityFilterModel.Temperature.Value);
            }

            if (cityFilterModel.Updated.HasValue)
            {
                items = items.Where(i => i.Updated == cityFilterModel.Updated.Value);
            }

            return items;
        }
    }
}
