using System.Threading.Tasks;
using Domain.Dto;
using Domain.Filtering;

namespace Services
{
    public interface ICityService
    {
        Task<IPagedList<CityDto>> GetAll(CityFilterModel cityFilterModel);
        Task<CityDto> GetById(int id);
        Task<CityDto> Create(CityDto cityDto);
        Task Update(CityDto cityDto);
        Task<CityDto> Delete(int id);
    }
}