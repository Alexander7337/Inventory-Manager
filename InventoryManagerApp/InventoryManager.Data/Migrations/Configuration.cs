namespace InventoryManager.Data.Migrations
{
    using InventoryManager.Entities.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InventoryManager.Data.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(InventoryManager.Data.AppContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!context.Roles.Any(role => role.Name == "Viewer"))
            {
                var role = new IdentityRole("Viewer");
                roleManager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            if (!context.Garments.Any())
            {
                Garment garment_1 = new Garment()
                {
                    Name = "Andrews",
                    Type = "Jeans",
                    Quantity = 10,
                    Size = "32",
                    Price = 45.90M,
                    ImageUrl = "/images/jeans.png",
                };

                Garment garment_2 = new Garment()
                {
                    Name = "Andrews",
                    Type = "Vest",
                    Quantity = 5,
                    Size = "M",
                    Price = 42M,
                    ImageUrl = "/images/vest.png"
                };

                Garment garment_3 = new Garment()
                {
                    Name = "Pierlucci",
                    Type = "Tee",
                    Quantity = 3,
                    Size = "M",
                    Price = 29M,
                    ImageUrl = "/images/tee.png",
                    Description = "Materials used: 98% cotton; 2% elastane. Made in the EU"
                };

                context.Garments.Add(garment_1);
                context.Garments.Add(garment_2);
                context.Garments.Add(garment_3);
            }
        }
    }
}
