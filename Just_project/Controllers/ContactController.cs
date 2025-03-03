using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
