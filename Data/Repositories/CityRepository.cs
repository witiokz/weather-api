using Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Filtering;
using Domain.Filtering.FilterExtensions;
using Data.Extensions;

namespace Data
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherAPIContext _context;

        public CityRepository(WeatherAPIContext context)
        {
            _context = context;
        }
        public async Task<IPagedList<City>> GetAll(CityFilterModel cityFilterModel)
        {
            var pageNumber = cityFilterModel != null ? cityFilterModel.PageNumber : 0;
            var pageSize = cityFilterModel != null ? cityFilterModel.PageSize : 0;

            return await _context.City.ApplyFilter(cityFilterModel)
                        .ToPagedListAsync(pageNumber, pageSize) 
                        .ConfigureAwait(false);
        }

        public async Task<City> GetById(int id)
        {
            return await _context.City.FindAsync(id);
        }

        public async Task<City> Create(City city)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return city;
        }

        public async Task Update(City city)
        {

            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<City> Delete(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city != null)
            {
                _context.City.Remove(city);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return city;
        }
    }
}
