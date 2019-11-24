using Domain.Dto;
using Domain.Filtering;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using Xunit;
using System.Linq;

namespace WeatherAPI.Tests
{
    public class CitiesControllerTests
    {
        private readonly ICityService _service;
        private readonly CitiesController _controller;

        public CitiesControllerTests()
        {
            _service = new CityServiceFakeService();
            _controller = new CitiesController(_service);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = await _controller.Get(new CityFilterModel());

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFound = await _controller.Get(0);

            // Assert
            Assert.IsType<NotFoundResult>(notFound.Result);
        }

        [Fact]
        public async Task GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<CityViewModel>(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CityViewModel();

            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = await _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new CityViewModel
            {
                Name = "NewCity"
            };

            // Act
            var createdResponse = await _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CityViewModel
            {
                Name = "NewCity"
            };

            // Act
            var createdResponse = await _controller.Post(testItem);
            var item = createdResponse.Result as CreatedAtActionResult;

            // Assert
            Assert.IsType<CityViewModel>(item.Value);

            var cityViewModel = item.Value as CityViewModel;
            Assert.Equal("NewCity", cityViewModel.Name);
        }

        [Fact]
        public async Task Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Act
            var badResponse = await _controller.Delete(0);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public async Task Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.NotNull(result.Value);
        }
    }
}
