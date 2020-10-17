using Comics.DTO;
using ComicsAPI.Controllers;
using ComicsAPI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsAPI.Tests
{
    [TestClass]
    public class CharactersControllerTest
    {
        private readonly Mock<ICharactersService> _charactersServiceMock = new Mock<ICharactersService>();
        private readonly Mock<ILogger<CharactersController>> _loggerMock = new Mock<ILogger<CharactersController>>();

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [DataTestMethod]
        [DataRow("a")]
        [DataRow("-20")]
        [DataRow("102")]
        public async Task When_GetAndLimitIsInvalid_Should_ReturnCodeAndStatus(string limit)
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { Limit = limit };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [TestMethod]
        public async Task When_GetAndOrderByIsInvalid_Should_ReturnCodeAndStatus()
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { OrderBy = "test" };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [DataTestMethod]
        [DataRow("1,a,2")]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11")]
        public async Task When_GetAndComicsIsInvalid_Should_ReturnCodeAndStatus(string comics)
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { OrderBy = "name", Comics = comics };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [DataTestMethod]
        [DataRow("1,a,2")]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11")]
        public async Task When_GetAndSeriesIsInvalid_Should_ReturnCodeAndStatus(string series)
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { OrderBy = "name", Series = series };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [DataTestMethod]
        [DataRow("1,a,2")]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11")]
        public async Task When_GetAndEventsIsInvalid_Should_ReturnCodeAndStatus(string events)
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { OrderBy = "name", Events = events };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [DataTestMethod]
        [DataRow("1,a,2")]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11")]
        public async Task When_GetAndStoriesIsInvalid_Should_ReturnCodeAndStatus(string stories)
        {
            // Arrange
            var controller = GetController();
            var request = new CharactersRequest() { Stories = stories };

            // Act
            var actual = await controller.Get(request);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(409, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [TestMethod]
        public async Task When_GetAndUnexpectedException_Should_ReturnResponse()
        {
            // Arrange
            var controller = GetController();
            _charactersServiceMock.Setup(x => x.GetCharacters(It.IsAny<CharactersRequest>(), It.IsAny<List<int>>(), It.IsAny<List<int>>(),
                It.IsAny<List<int>>(), It.IsAny<List<int>>(), It.IsAny<string[]>(), It.IsAny<int>()))
                .Throws(new System.Exception());

            // Act
            var actual = await controller.Get(null);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        [TestCategory(TestCategories.UNIT_TEST), TestCategory(TestCategories.WEBAPI_TEST), TestCategory(TestCategories.CONTROLLER_TEST)]
        [TestMethod]
        public async Task When_Get_Should_ReturnResponse()
        {
            // Arrange
            var controller = GetController();

            // Act
            var actual = await controller.Get(null);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.Code);
            Assert.IsFalse(string.IsNullOrEmpty(actual.Status));
        }

        private CharactersController GetController()
        {
            return new CharactersController(_charactersServiceMock.Object, _loggerMock.Object);
        }
    }
}
