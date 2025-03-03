using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class FaqsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
