using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using System.Diagnostics;

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
        public IActionResult AddComplist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComplist(CreateComplistViewModel model)
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

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> AddBlog(CreateBlogViewModel model)
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
