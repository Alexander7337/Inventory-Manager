namespace InventoryManager.Services
{
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using InventoryManager.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GarmentService : Service, IGarmentService
    {
        public IEnumerable<GarmentViewModel> GetProducts()
        {
            IEnumerable<Garment> garments = this.Context.Garments.ToList();
            IEnumerable<GarmentViewModel> garmentsVms = new List<GarmentViewModel>();
            garmentsVms = AutoMapper.Mapper.Map<IEnumerable<Garment>, IEnumerable<GarmentViewModel>>(garments);
            return garmentsVms;
        }

        public Garment GetGarmentByID(int id)
        {
            Garment garment = new Garment();
            garment = this.Context.Garments.FirstOrDefault(i => i.Id == id);
            return garment;
        }

        public GarmentEditBindingModel GetGarmentBindingModelByID(int id)
        {
            Garment garment = this.GetGarmentByID(id);
            GarmentEditBindingModel model = new GarmentEditBindingModel();
            model = AutoMapper.Mapper.Map<Garment, GarmentEditBindingModel>(garment);
            return model;
        }

        public void DeleteGarmentByID(int id)
        {
            Garment garment = new Garment();
            garment = this.Context.Garments.FirstOrDefault(i => i.Id == id);
            if (garment != null)
            {
                this.Context.Garments.Remove(garment);
                this.Context.SaveChanges();
            }
            
            //TO DO: Log that garment was null
        }

        public void EditGarmentByID(int id, GarmentEditBindingModel model)
        {
            Garment previousGarment = this.GetGarmentByID(id);
            if (previousGarment != null)
            {
                previousGarment.Description = model.Description;
                previousGarment.ImageUrl = model.ImageUrl;
                previousGarment.Name = model.Name;
                previousGarment.Price = model.Price;
                previousGarment.Quantity = model.Quantity;
                previousGarment.Size = model.Size;
                previousGarment.Type = model.Type;
            }

            this.Context.SaveChanges();
        }

        public void AddGarment(AddGarmentBindingModel model)
        {
            Garment newGarment = new Garment();
            newGarment = AutoMapper.Mapper.Map<AddGarmentBindingModel, Garment>(model);
            this.Context.Garments.Add(newGarment);
            this.Context.SaveChanges();
        }

        public IEnumerable<GarmentViewModel> SearchGarments(SearchGarmentBindingModel model)
        {
            IEnumerable<GarmentViewModel> garmentsVms = new List<GarmentViewModel>();
            IEnumerable<Garment> garments = new List<Garment>();

            if (model.Name != null && model.Type != null)
            {
                garments = this.Context.Garments.Where(g => g.Name.ToLower() == model.Name.ToLower() 
                                                        && g.Type.ToLower() == model.Type.ToLower());
            }
            else if (model.Name != null)
            {
                garments = this.Context.Garments.Where(g => g.Name.ToLower() == model.Name.ToLower());
            }
            else if (model.Type != null)
            {
                garments = this.Context.Garments.Where(g => g.Type.ToLower() == model.Type.ToLower());
            }

            garmentsVms = AutoMapper.Mapper.Map<IEnumerable<Garment>, IEnumerable<GarmentViewModel>>(garments);

            return garmentsVms;
        }
    }
}
