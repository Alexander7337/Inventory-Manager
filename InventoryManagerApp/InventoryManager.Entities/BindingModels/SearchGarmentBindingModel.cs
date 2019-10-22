namespace InventoryManager.Entities.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class SearchGarmentBindingModel
    {
        [RegularExpression(@"([A-Za-z\w\s]+)", ErrorMessage = "Please use only Latin letters")]
        public string Name { get; set; }

        [RegularExpression(@"([A-Za-z\w\s]+)", ErrorMessage = "Please use only Latin letters")]
        public string Type { get; set; }
    }
}
