using Just_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


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
                    ImagePath = b.ImagePath,
                    CreateTime = b.CreateAt,

                };
            }).ToList();


            return View(BlogViewModels);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> AddBlog(CreateBlogViewModel model)
        {
            byte[] imageData = null;
            if (model.ImagePath != null) {
                using (var memoryStream = new MemoryStream()) {
                    await model.ImagePath.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            
            }
            var blog = new BlogModel
            {
                ImagePath = imageData,
                BlogTranslations = new List<BlogTranslationModel> {
                    new BlogTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en },
                    new BlogTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru },
                    new BlogTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk },
                }
            };

            _db.Blogs.Add(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
