using CarServiceMVCApp.Controllers;
using CarServiceMVCApp.Data;
using CarServiceMVCApp.Models;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceMVCApp.Tests
{
    [TestFixture]
    public class CarsControllerTests
    {
        private ApplicationDbContext mockContext;

        private CarsController controller;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            mockContext = Create.MockedDbContextFor<ApplicationDbContext>();

            var cars = new List<Car>
            {
                new() { Id = 1, Year = 2020, Make = "Lambo", RegistrationNumber = "abc123"},
                new() { Id = 2, Year = 2021, Make = "Volvo", RegistrationNumber = "def567"},
                new() { Id = 3, Year = 2022, Make = "Fiat", RegistrationNumber = "ghi789"},
            };

            mockContext.Set<Car>().AddRange(cars);
            mockContext.SaveChanges();

            mockContext.ChangeTracker.Clear();

            controller = new CarsController(mockContext);
        }

        [Test]
        public async Task Index_ReturnsViewResult_WithListOfCars()
        {
            // Act
            var result = await controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var viewResult = result as ViewResult;
            var model = viewResult.Model;
            Assert.That(model, Is.InstanceOf<List<Car>>());

            var carList = model as List<Car>;
            Assert.That(carList.Count, Is.EqualTo(3));
        }

        [Test]
        public void Create_Get_ReturnsViewResult()
        {
            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var newCar = new Car() { Year = 2022, Make = "Bmw", RegistrationNumber = "bmw404" };

            // Act
            var result = await controller.Create(newCar);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var actionResult = result as RedirectToActionResult;
            Assert.That(actionResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Error", "Model error");
            var newCar = new Car() { Year = 2022, Make = "Bmw", RegistrationNumber = "bmw404" };

            // Act
            var result = await controller.Create(newCar);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;

            Assert.That(viewResult.Model, Is.InstanceOf<Car>());
            var model = viewResult.Model as Car;

            Assert.That(model.RegistrationNumber, Is.EqualTo(newCar.RegistrationNumber));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }


        [Test]
        public async Task Edit_Get_ReturnsNotFoundResult_WhenSongDoesNotExist()
        {
            // Act
            var result = await controller.Edit(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }


        [Test]
        public async Task Edit_Get_ReturnsViewResult_WithCar()
        {
            // Act 
            var result = await controller.Edit(1);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;

            Assert.That(viewResult.Model, Is.InstanceOf<Car>());
            var model = viewResult.Model as Car;

            Assert.That(model.Id, Is.EqualTo(1));
        }


        [Test]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var car = new Car() { Id = 1, Year = 2022, Make = "Bmw", RegistrationNumber = "bmw404" };

            // Act
            var result = await controller.Edit(1, car);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }
    }
}
