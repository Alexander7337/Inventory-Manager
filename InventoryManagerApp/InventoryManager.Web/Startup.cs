using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryManager.Web.Startup))]

namespace InventoryManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}