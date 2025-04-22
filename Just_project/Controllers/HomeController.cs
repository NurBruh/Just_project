using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Just_project.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Just_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>? _logger;
        private readonly LocalizeDbContext _context;
        private readonly AppDbContext _db;
        public HomeController(ILogger<HomeController>? logger, LocalizeDbContext context, AppDbContext db)
        {
            _logger = logger;
            _context = context;
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
                    
                };
            }).ToList();

            var model = new HomePageModels
            {
                Components = ComponentsViewModels,
                Blogs = BlogViewModels
            };
            return View(model);
        }
        public IActionResult Tester(int id)
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;

            var pc = _db.Pcs
                .Include(p => p.PcTranslations)
                .FirstOrDefault(p => p.Id == id);

            var localized = pc.PcTranslations.FirstOrDefault(t => t.Language == currentCulture);

            var title = localized?.Title ?? "Default title";
            var description = localized?.Description ?? "Default description";

            return View();
        }
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
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

        public IActionResult PcList()
        {
            var culture = CultureInfo.CurrentUICulture.Name;

            var pcs = _db.Pcs
                .Include(p => p.PcTranslations)
                .ToList();

            var PcViewModels = pcs.Select(p =>
            {
                var translation = p.PcTranslations.FirstOrDefault(t => t.Language == culture);

                return new PcViewModel
                {
                    Id = p.Id,
                    Title = translation?.Title ?? "",
                    Description = translation?.Description ?? "",
                    Price = p.Price

                };
            }).ToList();

            return View(PcViewModels);
        }

        public IActionResult LocalizedText()
        {
            var culture = Thread.CurrentThread.CurrentUICulture.Name;

            var text = _context.LocalizedStrings
                .FirstOrDefault(x => x.Key == "ProductDescription" && x.Language == culture)
                ?.Value ?? "No translation";

            ViewBag.LocalizedText = text;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
       

        [HttpPost]
        public JsonResult ChangeCulture(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),

                new CookieOptions { Expires = DateTime.Now.AddMonths(1) }
                );

            return Json(culture);
        }
    }
}
