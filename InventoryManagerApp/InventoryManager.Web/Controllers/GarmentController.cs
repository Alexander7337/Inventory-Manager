namespace InventoryManager.Web.Controllers
{
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Services.Contracts;
    using System.Web.Mvc;

    public class GarmentController : Controller
    {
        private IGarmentService _service;

        public GarmentController(IGarmentService service)
        {
            this._service = service;
        }

        // GET: Garment/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Garment garment = this._service.GetGarmentByID(id);
            return View(garment);
        }

        // GET: Garment/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Garment/Create
        [HttpPost]
        public ActionResult Create(AddGarmentBindingModel model)
        {
            try
            {
                if (model != null)
                {
                    this._service.AddGarment(model);
                    return RedirectToAction("LandingPage", "Home");
                }

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: Garment/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            GarmentEditBindingModel garment = this._service.GetGarmentBindingModelByID(id);
            if (garment != null)
            {
                return View(garment);
            }

            return RedirectToAction("LandingPage", "Home");
        }

        // POST: Garment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GarmentEditBindingModel model)
        {
            try
            {
                this._service.EditGarmentByID(id, model);
                return RedirectToAction("LandingPage", "Home");
            }
            catch
            {
                return View();
            }
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._service.DeleteGarmentByID(id);
                return RedirectToAction("LandingPage", "Home");
            }
            catch
            {
                return View("Details", "Garment", new { garmentId = id });
            }
        }
    }
}
