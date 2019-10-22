namespace InventoryManager.Data
{
    using InventoryManager.Data.Migrations;
    using InventoryManager.Entities.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class AppContext : IdentityDbContext<ApplicationUser>
    {
        public AppContext()
            : base("name=AppContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext, Configuration>());
        }
        
        public virtual DbSet<Garment> Garments { get; set; }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}