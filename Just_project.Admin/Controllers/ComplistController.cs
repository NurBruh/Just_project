using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
namespace Just_project.Admin.Controllers
{
    [Authorize]
    public class ComplistController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5199/api/ComplistAPI";

        public ComplistController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> ComplistList()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/getAll");
            if (!response.IsSuccessStatusCode)
                return View(new List<ComplistTranslationModel>());

            var json = await response.Content.ReadAsStringAsync();
            var complists = JsonConvert.DeserializeObject<List<ComplistTranslationModel>>(json);
            return View(complists);
        }

        public IActionResult AddComplist() => View();

        [HttpPost]
        public async Task<IActionResult> AddComplist(CreateComplistViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Title_en ?? ""), "Title_en");
            formData.Add(new StringContent(model.Title_ru ?? ""), "Title_ru");
            formData.Add(new StringContent(model.Title_kk ?? ""), "Title_kk");

            if (model.ImagePath != null)
            {
                using var ms = new MemoryStream();
                await model.ImagePath.CopyToAsync(ms);
                formData.Add(new ByteArrayContent(ms.ToArray())
                {
                    Headers = { ContentType = MediaTypeHeaderValue.Parse(model.ImagePath.ContentType) }
                }, "ImagePath", model.ImagePath.FileName);
            }

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/createComplist", formData);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("ComplistList");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        public async Task<IActionResult> EditComplist(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/getById/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("ComplistList");

            var json = await response.Content.ReadAsStringAsync();
            var translations = JsonConvert.DeserializeObject<List<ComplistTranslationModel>>(json);

            var model = new UpdateComplistViewModel
            {
                Id = id,
                Title_en = translations.FirstOrDefault(t => t.Language == "en-US")?.Title,
                Title_ru = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Title,
                Title_kk = translations.FirstOrDefault(t => t.Language == "kk-KZ")?.Title,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditComplist(UpdateComplistViewModel model)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Title_en ?? ""), "Title_en");
            formData.Add(new StringContent(model.Title_ru ?? ""), "Title_ru");
            formData.Add(new StringContent(model.Title_kk ?? ""), "Title_kk");

            if (model.NewImage != null)
            {
                using var ms = new MemoryStream();
                await model.NewImage.CopyToAsync(ms);
                formData.Add(new ByteArrayContent(ms.ToArray())
                {
                    Headers = { ContentType = MediaTypeHeaderValue.Parse(model.NewImage.ContentType) }
                }, "NewImage", model.NewImage.FileName);
            }

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/update", formData);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("ComplistList");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        // GET: Complist/DeleteComplist/5
        [HttpGet]
        public async Task<IActionResult> DeleteComplist(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/getById/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("ComplistList");

            var json = await response.Content.ReadAsStringAsync();
            var translations = JsonConvert.DeserializeObject<List<ComplistTranslationModel>>(json);
            var ru = translations.FirstOrDefault(t => t.Language == "ru-RU");

            return View("DeleteComplist", ru);
        }

        // POST: Complist/DeleteComplistConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteComplistConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"{_apiBaseUrl}/delete/{id}");
            return RedirectToAction("ComplistList");
        }

    }
}
