namespace InventoryManager.Services
{
    public class Service
    {
        public Service()
        {
            this.Context = InventoryManager.Data.AppContext.Create();
        }

        protected InventoryManager.Data.AppContext Context { get; }
    }
}
