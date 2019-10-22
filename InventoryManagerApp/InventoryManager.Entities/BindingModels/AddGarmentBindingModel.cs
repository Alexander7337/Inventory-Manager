namespace InventoryManager.Entities.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddGarmentBindingModel
    {
        [Required]
        [RegularExpression(@"([A-Za-z\w\s]+)", ErrorMessage = "Please use only Latin letters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"([A-Za-z\w\s]+)", ErrorMessage = "Please use only Latin letters")]
        public string Type { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity cannot be a negative number or zero")]
        public int Quantity { get; set; }

        [Required]
        [RegularExpression(@"([A-Za-z]+)|([0-9]+)", ErrorMessage = "Please only Latin letters or digits")]
        public string Size { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please type a valid number")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [RegularExpression(@"([A-Za-z\w\s]+)\.*", ErrorMessage = "Please only Latin letters")]
        public string Description { get; set; }
    }
}
