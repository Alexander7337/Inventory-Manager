namespace InventoryManager.Tests
{
    using System.Linq;
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using InventoryManager.Services;
    using InventoryManager.Services.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HomeControllerTests
    {
        private static bool isMapperInitialized;
        private IGarmentService service;
        private SearchGarmentBindingModel searchModel;
        private IUserService uService;
        
        [TestInitialize]
        public void Init()
        {
            if (!isMapperInitialized)
            {
                ConfigureMappings();
            }
            service = new GarmentService();
            uService = new UserService();
            searchModel = new SearchGarmentBindingModel();
        }

        [TestMethod]
        public void WhenSearchWithValidName_ThenReturnValidGarments()
        {
            searchModel.Name = "Andrews";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Name == "Andrews").Count();

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithInvalidName_ThenReturnZeroGarments()
        {
            searchModel.Name = "z";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = 0;

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithValidType_ThenReturnValidGarments()
        {
            searchModel.Type = "jeans";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Type.ToLower() == "jeans").Count();

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithValidData_ThenReturnValidGarments()
        {
            searchModel.Name = "andrews";
            searchModel.Type = "jeans";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Type.ToLower() == "jeans" 
                                    && e.Name.ToLower() == "andrews").Count();

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithInvalidData_ThenReturnZeroGarments()
        {
            searchModel.Name = "mojojojo";
            searchModel.Type = "jeans";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Type.ToLower() == "jeans"
                                    && e.Name.ToLower() == "mojojojo").Count();

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithCyrillicData_ThenReturnZeroGarments()
        {
            searchModel.Type = "jеаns";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Type.ToLower() == "jеаns").Count();

            Assert.AreEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        [TestMethod]
        public void WhenSearchWithCyrillicAndLatinData_ThenReturnZeroGarments()
        {
            searchModel.Type = "jеаns";
            var actualResult = service.SearchGarments(searchModel).Count();
            var expectedResult = service.GetProducts().Where(e => e.Type.ToLower() == "jeans").Count();

            Assert.AreNotEqual(expectedResult, actualResult, $"Actual result differs from expected result: {actualResult} != {expectedResult}");
        }

        private static void ConfigureMappings()
        {
            isMapperInitialized = true;
            AutoMapper.Mapper.Initialize(expression =>
            {
                expression.CreateMap<Garment, GarmentViewModel>()
                .ForMember(vm => vm.Id, g => g.MapFrom(i => i.Id))
                .ForMember(vm => vm.Name, g => g.MapFrom(n => n.Name))
                .ForMember(vm => vm.Price, g => g.MapFrom(p => p.Price))
                .ForMember(vm => vm.Quantity, g => g.MapFrom(gty => gty.Quantity))
                .ForMember(vm => vm.Type, g => g.MapFrom(t => t.Type));

                expression.CreateMap<Garment, GarmentEditBindingModel>();

                expression.CreateMap<AddGarmentBindingModel, Garment>()
                .ForMember(g => g.Description, bm => bm.MapFrom(d => d.Description))
                .ForMember(g => g.ImageUrl, bm => bm.MapFrom(e => e.ImageUrl))
                .ForMember(g => g.Name, bm => bm.MapFrom(n => n.Name))
                .ForMember(g => g.Price, bm => bm.MapFrom(p => p.Price))
                .ForMember(g => g.Quantity, bm => bm.MapFrom(gty => gty.Quantity))
                .ForMember(g => g.Type, bm => bm.MapFrom(t => t.Type))
                .ForMember(g => g.Size, bm => bm.MapFrom(s => s.Size))
                .ForMember(g => g.Id, bm => bm.Ignore());
            });
        }
    }
}
