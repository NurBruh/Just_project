using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RedirectToCheckout()
        {
            return RedirectToRoute("default", new { controller = "Checkout", action = "Index" });
        }
    }
}
