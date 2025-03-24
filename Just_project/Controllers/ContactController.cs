using Just_project.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Just_project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        public IActionResult Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                // Логика обработки данных формы (например, отправка сообщения)
                model.IsSuccess = true;
                Log.Logger.Debug("Contact form submitted: {@ContactModel}", model);
                ModelState.Clear(); 
                return View(model);
            }
            var errors = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            Log.Logger.Debug("Validation failed: {@Errors}", errors);

            return View(model);
        }
    }
}
