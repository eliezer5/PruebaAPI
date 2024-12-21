using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using PruebaAPI.Controllers;
using Xunit;
namespace TestPrueba.Controller
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            // Mock del ILogger
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();

            // Instancia del controlador con el mock del logger
            _controller = new WeatherForecastController(_loggerMock.Object);
        }

        [Fact]
        public void Get_ReturnsExpectedNumberOfItems()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ReturnsValidWeatherForecastObjects()
        {
            // Act
            var result = _controller.Get();

            // Assert
            foreach (var forecast in result)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Contains(forecast.Summary, WeatherForecastController.Summaries);
                Assert.IsType<DateOnly>(forecast.Date);
            }
        }
    }
}
