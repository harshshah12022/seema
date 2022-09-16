using AspCoreApiDapperProject.Controllers;
using AspCoreApiDapperProject.Models;
using AspCoreApiDapperProject.Service;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesForPOC.Controllers
{
    public class EmployeeControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IEmployeeService> mockEmployeeService;
        private Mock<ILogger<EmployeeController>> mockLogger;
        private Mock<IMapper> mockMapper;

        public EmployeeControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockEmployeeService = this.mockRepository.Create<IEmployeeService>();
            this.mockLogger = this.mockRepository.Create<ILogger<EmployeeController>>();
            this.mockMapper = this.mockRepository.Create<IMapper>();
        }

        private EmployeeController CreateEmployeeController()
        {
            return new EmployeeController(
                this.mockEmployeeService.Object,
                this.mockLogger.Object,
                this.mockMapper.Object);
        }

        //[Fact]
        //public async Task GetAll_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var employeeController = this.CreateEmployeeController();
        //    int page=0; int pagesize=0;
        //    // Act
        //    var result = await employeeController.GetAll(page,pagesize);

        //    // Assert
        //    Assert.True(false);
        //    this.mockRepository.VerifyAll();
        //}

        [Fact]
        public async Task Search_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            string text = null;

            // Act
            var result = await employeeController.Search(
                text);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int id = 0;

            // Act
            var result = await employeeController.GetById(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            Employee employee = null;

            // Act
            var result = await employeeController.Create(
                employee);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            Employee employee = null;

            // Act
            var result = await employeeController.Update(
                employee);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int id = 0;

            // Act
            var result = await employeeController.Delete(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
