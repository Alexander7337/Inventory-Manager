namespace InventoryManager.Web
{
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureMappings()
        {
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
