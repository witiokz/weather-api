using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using Xunit;

namespace WeatherAPI.Tests
{
    public class TemperaturesControllerTests
    {
        private readonly TemperaturesController _controller;
        private readonly ICityService _cityService;
        private readonly ITemperatureService _temperatureService;

        public TemperaturesControllerTests()
        {
            _cityService = new CityServiceFakeService();
            _temperatureService = new TemperatureServiceFakeService();
            _controller = new TemperaturesController(_cityService, _temperatureService);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsTemperatureByCityId()
        {
            //Arrange
            var cityId = 1;

            // Act
            var result = await _controller.Get(cityId);

            // Assert
            Assert.IsType<TemperatureViewModel>(result.Value);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var cityId = 0;

            var incorrectCityIdItem = new TemperatureDto
            {
                CityId = 1,
                Temperature = 100
            };
            // Act
            var badResponse = await _controller.Post(cityId, incorrectCityIdItem);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse.Result);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var cityId = 1;


            var testItem = new TemperatureDto
            {
                CityId = 1,
                Temperature = 100
            };

            // Act
            var createdResponse = await _controller.Post(cityId, testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }

        [Fact]
        public async Task Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var id = 0;

            var incorrectCityIdItem = new TemperatureDto
            {
                CityId = 1,
                Temperature = 100
            };

            // Act
            var badResponse = await _controller.Put(id, incorrectCityIdItem);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async Task Update_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var id = 1;

            var existingItem = new TemperatureDto
            {
                CityId = 1,
                Temperature = 100
            };

            // Act
            var createdResponse = await _controller.Put(id, existingItem);

            // Assert
            Assert.IsType<NoContentResult>(createdResponse);
        }
    }
}
