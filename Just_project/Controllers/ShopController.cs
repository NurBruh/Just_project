using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Just_project.Models;
using Microsoft.EntityFrameworkCore;
namespace Just_project.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _db;
        public ShopController(AppDbContext db)
        {
            
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentUICulture.Name;

            var blogs = _db.Blogs
                 .Include(b => b.BlogTranslations).ToList();

            var BlogViewModels = blogs.Select(b =>
            {
                var translation = b.BlogTranslations.FirstOrDefault(
                t => t.Language == culture);
                return new BlogViewModel
                {
                    Id = b.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    ImagePath = b.ImagePath,
                    CreateTime = b.CreateAt,

                };
            }).ToList();

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
                Blogs = BlogViewModels,
                Complists = ComplistViewModels,
                PCs = PcViewModels
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult RedirectToShop()
        {
            return RedirectToRoute("default", new { controller = "Shop", action = "Index" });
        }
    }
}
