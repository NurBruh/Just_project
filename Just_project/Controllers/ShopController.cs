using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
