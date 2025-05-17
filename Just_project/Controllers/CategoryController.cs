using Just_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Just_project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(
                    t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = p.ImagePath,
                    Price = p.Price
                };
            }).ToList();
            return View(PcViewModels);
        }

        public IActionResult Components()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var components = _db.Components
                .Include(c => c.ComponentsTranslations).ToList();

            var ComponentsViewModels = components.Select(c =>
            {
                var translation = c.ComponentsTranslations.FirstOrDefault(
                    t => t.Language == culture);
                return new ComponentsViewModel
                {
                    Id = c.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    Price = c.Price,
                    ImagePath = c.ImagePath

                };
            }).ToList();

            var complists = _db.Complists
                .Include(c => c.ComplistTranslations).ToList();
            var ComplistViewModels = complists.Select(c =>
            {
                var traslation = c.ComplistTranslations.FirstOrDefault(
                    t => t.Language == culture);
                return new ComplistViewModel
                {
                    Id = c.Id,
                    Title = traslation?.Title ?? "",
                    ImagePath = c.ImagePath,
                };
            }).ToList();

            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(
                    t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = p.ImagePath,
                    Price = p.Price
                };
            }).ToList();


            var model = new HomePageModels
            {
                Components = ComponentsViewModels,
                
                Complists = ComplistViewModels,
                PCs = PcViewModels
            };
            return View(model);
        }

        public IActionResult Ready()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(
                    t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = p.ImagePath,
                    Price = p.Price
                };
            }).ToList();
            return View(PcViewModels);
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
            var culture = CultureInfo.CurrentUICulture.Name;
            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(
                    t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = p.ImagePath,
                    Price = p.Price
                };
            }).ToList();
            return View(PcViewModels);
        }
        public IActionResult Office()
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(
                    t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = p.ImagePath,
                    Price = p.Price
                };
            }).ToList();
            return View(PcViewModels);
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
