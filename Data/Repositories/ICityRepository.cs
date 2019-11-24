using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Filtering;

namespace Data
{
    public interface ICityRepository
    {
        Task<IPagedList<City>> GetAll(CityFilterModel cityFilterModel);
        Task<City> GetById(int id);
        Task<City> Create(City city);
        Task Update(City city);
        Task<City> Delete(int id);
    }
}