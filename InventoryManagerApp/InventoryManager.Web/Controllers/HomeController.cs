namespace InventoryManager.Web.Controllers
{
    using InventoryManager.Entities.BindingModels;
    using InventoryManager.Entities.Models;
    using InventoryManager.Entities.ViewModels;
    using InventoryManager.Services;
    using InventoryManager.Services.Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private IGarmentService service = new GarmentService();
        private IUserService uService = new UserService();
        private ApplicationSignInManager _signInManage;
        private ApplicationUserManager _userManager;

        public HomeController(IGarmentService service, IUserService uService)
        {
            this.service = service;
            this.uService = uService;
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManage ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManage = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult LandingPage(string sortOrder, SearchGarmentBindingModel model)
        {
            IEnumerable<GarmentViewModel> garments = new List<GarmentViewModel>();
            garments = TempData["garmentsVms"] as List<GarmentViewModel>;

            if (model != null && (model.Name != null || model.Type != null))
            {
                return RedirectToAction("Search", model);
            }
            
            if (garments == null)
            {
                garments = this.service.GetProducts();
            }

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.QuantitySortParam = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";

            switch (sortOrder)
            {
                case "name_desc":
                    garments = garments.OrderByDescending(e => e.Name);
                    break;
                case "Price":
                    garments = garments.OrderBy(e => e.Price);
                    break;
                case "price_desc":
                    garments = garments.OrderByDescending(e => e.Price);
                    break;
                case "Type":
                    garments = garments.OrderBy(e => e.Type);
                    break;
                case "type_desc":
                    garments = garments.OrderByDescending(e => e.Type);
                    break;
                case "Quantity":
                    garments = garments.OrderBy(e => e.Quantity);
                    break;
                case "quantity_desc":
                    garments = garments.OrderByDescending(e => e.Quantity);
                    break;
                default:
                    garments = garments.OrderBy(e => e.Name);
                    break;
            }

            return View(garments);
        }

        // GET: /Home/Search
        public ActionResult Search(SearchGarmentBindingModel model)
        {
            IEnumerable<GarmentViewModel> garments = new List<GarmentViewModel>();
            garments = this.service.SearchGarments(model);
            TempData["garmentsVms"] = garments;
            return RedirectToAction("LandingPage", "Home");
        }

        // GET: /Home/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                ApplicationUser user = userManager.Find(login.Email.Split('@')[0], login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(ViewBag.ReturnUrl ?? Url.Action("LandingPage", "Home"));
                }
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(login);
        }

        // GET: /Home/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Home/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0]
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (this.uService.IsFirstRegistration())
                    {
                        this.UserManager.AddToRole(user.Id, "Admin");
                    }
                    else
                    {
                        this.UserManager.AddToRole(user.Id, "Viewer");
                    }

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("LandingPage", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // POST: /Home/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LandingPage", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}