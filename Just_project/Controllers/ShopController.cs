using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RedirectToShop()
        {
            return RedirectToRoute("default", new { controller = "Shop", action = "Index" });
        }
    }
}
