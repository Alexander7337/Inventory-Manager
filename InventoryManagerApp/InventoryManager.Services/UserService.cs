namespace InventoryManager.Services
{
    using InventoryManager.Services.Contracts;
    using System.Linq;

    public class UserService : Service, IUserService
    {
        public bool IsFirstRegistration()
        {
            if (this.Context.Users.Count() == 1)
            {
                return true;
            }
            return false;
        }
    }
}
