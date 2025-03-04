using System.Diagnostics;
using Just_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Just_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMessage _sender;

        public HomeController(ILogger<HomeController> logger, IMessage sender)
        {
            _logger = logger;
            _sender = sender;
        }

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
        public ActionResult Contact()
        {
            return View();
        }
        public IActionResult Contact(Message userMessage)
        {
            MessageValidator rules = new MessageValidator();
            var result = rules.Validate(userMessage);
            if (result.IsValid)
            {

            }
            return View(userMessage);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
