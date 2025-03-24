using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Carpenter()
        {
            return View();
        }

        public IActionResult Flathead()
        {
            return View();
        }

        public IActionResult Hand()
        {
            return View();
        }
        public IActionResult Painting()
        {
            return View();
        }
        public IActionResult Plumbing()
        {
            return View();
        }
        public IActionResult Power()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RedirectToCarpenter()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Carpenter" });
        }

        [HttpPost]
        public IActionResult RedirectToPainting()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Painting" });
        }

        [HttpPost]
        public IActionResult RedirectToPower()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Power" });
        }

        [HttpPost]
        public IActionResult RedirectToHand()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Hand" });
        }

        [HttpPost]
        public IActionResult RedirectToPlumbing()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Plumbing" });
        }

        [HttpPost]
        public IActionResult RedirectToFlathead()
        {
            return RedirectToRoute("default", new { controller = "Category", action = "Flathead" });
        }
    }


}
