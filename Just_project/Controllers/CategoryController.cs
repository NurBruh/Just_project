using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Components()
        {
            return View();
        }

        public IActionResult Ready()
        {
            return View();
        }

        public IActionResult Custom()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Gaming()
        {
            return View();
        }
        public IActionResult Office()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RedirectToComponents()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Components" });
        }

        [HttpPost]
        public IActionResult RedirectToCustom()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Custom" });
        }

        [HttpPost]
        public IActionResult RedirectToGaming()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Gaming" });
        }

        [HttpPost]
        public IActionResult RedirectToOffice()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Office" });
        }

        [HttpPost]
        public IActionResult RedirectToReady()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Ready" });
        }

        [HttpPost]
        public IActionResult RedirectToService()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Service" });
        }
    }


}
