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
                    Price = c.Price

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
        [HttpGet]
        public IActionResult AddComplist() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComplist(CreateComplistViewModel model)
        {
            byte[] imageData = null;
            if(model.ImagePath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImagePath.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            var complist = new ComplistModel
            {
                ImagePath = imageData,
                ComplistTranslations = new List<ComplistTranslationModel>
                {
                    new ComplistTranslationModel { Language = "en-US", Title = model.Title_en },
                    new ComplistTranslationModel { Language = "ru-RU", Title = model.Title_ru},
                    new ComplistTranslationModel { Language = "kk-KZ", Title = model.Title_kk },
                }
            };
            _db.Complists.Add(complist);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
        

        [HttpPost]
        public async Task<IActionResult> AddPc(CreatePcViewModel model)
        {
            byte[] imageData = null;
            if (model.ImagePath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImagePath.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }
            var pc = new PcModel
            {
                Price = model.Price,
                ImagePath = imageData,
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
        [HttpGet]
        public IActionResult AddComponents()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComponents(CreateComponentsViewModel model)
        {

            var component = new ComponentsModel
            {
                Price = model.Price,
                ComponentsTranslations = new List<ComponentsTranslationModel>
                {
                    new ComponentsTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en},
                    new ComponentsTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru},
                    new ComponentsTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk},
                }
            };

            _db.Components.Add(component);
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
