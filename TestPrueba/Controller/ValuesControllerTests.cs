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
    public class ValuesControllerTests
    {
        private readonly Mock<ILogger<ValuesController>> _loggerMock;
        private readonly ValuesController _controller;

        public ValuesControllerTests()
        {
            _loggerMock = new Mock<ILogger<ValuesController>>();

            // Instancia del controlador con el mock del logger
            _controller = new ValuesController(_loggerMock.Object);
        }

        [Fact]
        public void Get_ReturnsValidForeCastObjects()
        {
            // Act
            var result = _controller.Get();

            // Assert
            foreach (var forecast in result)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.Contains(forecast.Summary, ValuesController.Summaries);
                Assert.IsType<DateOnly>(forecast.Date);
            }
        }

        [Fact]
        public void Get_ReturnsExpectedItems()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }
    }
}
