using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;

namespace WeatherAPI.Tests
{
    public class StaticData
    {
        public static List<CityDto> GetData()
        {
            return new List<CityDto>
            {
                new CityDto { Id = 1, Name = "City1", Temperature = 10, Updated = DateTime.UtcNow.AddDays(-2) },
                new CityDto { Id = 2, Name = "City2", Temperature = 20, Updated = DateTime.UtcNow.AddDays(-2) },
                new CityDto { Id = 3, Name = "City3", Temperature = 30, Updated = DateTime.UtcNow.AddDays(1) },
            };
        }
    }
}
