using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
