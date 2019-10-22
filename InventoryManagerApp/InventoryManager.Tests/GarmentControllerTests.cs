namespace InventoryManager.Tests
{
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using InventoryManager.Services;
    using InventoryManager.Services.Contracts;
    using InventoryManager.Web.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Web.Mvc;

    [TestClass]
    public class GarmentControllerTests
    {
        private IGarmentService service;
        private GarmentController controller;

        [TestInitialize]
        public void Init()
        {
            this.service = new GarmentService();
            this.controller = new GarmentController(service);
        }

        [TestMethod]
        public void WhenViewDetails_ThenReturnCorrectModelOrView()
        {
            var result = (ViewResult)controller.Details(1);
            var model = result.Model;
            if (model != null)
            {
                var garment = (Garment)model;
                var actualResult = garment.Id;
                var expectedResult = 1;
                Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
            }
            else
            {
                int id = 1;
                var ids = service.GetProducts().Select(e => e.Id).ToList();
                bool isInDatabase = ids.Contains(id);
                Assert.AreEqual(false, isInDatabase, $"Actual result differs from expected result: {isInDatabase} != {false}");
            }
        }
    }
}
