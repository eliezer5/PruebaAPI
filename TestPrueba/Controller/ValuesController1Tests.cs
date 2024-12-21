using Microsoft.Extensions.Logging;
using Moq;
using PruebaAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPrueba.Controller
{
    public class ValuesController1Tests
    {
        private readonly Mock<ILogger<ValuesController1>> _loggerMock;
        private readonly ValuesController1 _controller;

        public ValuesController1Tests()
        {
            _loggerMock = new Mock<ILogger<ValuesController1>>();

            // Instancia del controlador con el mock del logger
            _controller = new ValuesController1(_loggerMock.Object);
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
                Assert.Contains(forecast.Summary, ValuesController1.Summaries);
                Assert.IsType<DateOnly>(forecast.Date);
            }
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
    }
}
