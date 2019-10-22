namespace InventoryManager.Services.Contracts
{
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using System.Collections.Generic;

    public interface IGarmentService
    {
        IEnumerable<GarmentViewModel> GetProducts();

        Garment GetGarmentByID(int id);

        GarmentEditBindingModel GetGarmentBindingModelByID(int id);

        void DeleteGarmentByID(int id);

        void EditGarmentByID(int id, GarmentEditBindingModel model);

        void AddGarment(AddGarmentBindingModel model);

        IEnumerable<GarmentViewModel> SearchGarments(SearchGarmentBindingModel model);

    }
}
