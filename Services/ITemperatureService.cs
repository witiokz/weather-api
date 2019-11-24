using Domain.Dto;
using System.Threading.Tasks;

namespace Services
{
    public interface ITemperatureService
    {
        Task<TemperatureDto> GetTemperatureByCityId(int cityId);

        Task<CityDto> Modify(TemperatureDto temperatureDto);
    }
}
