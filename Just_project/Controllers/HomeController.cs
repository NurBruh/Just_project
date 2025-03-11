using System.Diagnostics;
using Just_project.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>? _logger;
        private readonly IMessage? _sender;

        //public HomeController(ILogger<HomeController> logger, IMessage sender)
        //{
        //    _logger = logger;
        //    _sender = sender;
        //}

        public IActionResult Index()
        {
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
        //public ActionResult Contact()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Contact(Message userMessage)
        //{
        //    _logger.LogInformation("", DateTime.Now, userMessage.name, userMessage.Email, userMessage.Phone);


        //    MessageValidator rules = new MessageValidator();
        //    var result = rules.Validate(userMessage);

        //    var errors = result.Errors;

        //    if (result.IsValid)
        //    {
        //        _sender.sendMessage(userMessage.Email, userMessage.message, "New Message");
        //        return View();
        //    }
        //    else
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            _logger.LogError("", DateTime.Now, userMessage.name, item.ErrorMessage);
        //        }
        //    }
        //    return View(userMessage);
        //}
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
