namespace InventoryManager.Entities.BindingModels
{
    public class GarmentEditBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
