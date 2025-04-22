using Just_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;


namespace Just_project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
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

                };
            }).ToList();


            return View(BlogViewModels);
        }
    }
}
