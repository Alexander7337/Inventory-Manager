namespace InventoryManager.Entities.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Garment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
