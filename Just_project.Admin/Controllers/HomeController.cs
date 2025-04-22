using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;   

namespace Just_project.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public IActionResult AddPc()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(CreateBlogViewModel model)
        {
            var blog = new BlogModel
            {
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

        [HttpPost]
        public IActionResult AddPc(CreatePcViewModel model)
        {
            var pc = new PcModel
            {
                Price = model.Price,
                PcTranslations = new List<PcTranslationModel>
                {
                    new PcTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en },
                    new PcTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru },
                    new PcTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk },
                }
            };

            _db.Pcs.Add(pc);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        

        
        public IActionResult Position()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
