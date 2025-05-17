using Just_project.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Just_project.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5199/api/BlogAPI"; // замени на свой адрес

        public BlogController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> BlogList()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/getAll");
            if (!response.IsSuccessStatusCode)
                return View(new List<BlogTranslationModel>());

            var json = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<BlogTranslationModel>>(json);
            return View(blogs);
        }

        public IActionResult AddBlog() => View();

        [HttpPost]
        public async Task<IActionResult> AddBlog(CreateBlogViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Title_en ?? ""), "Title_en");
            formData.Add(new StringContent(model.Description_en ?? ""), "Description_en");
            formData.Add(new StringContent(model.Title_ru ?? ""), "Title_ru");
            formData.Add(new StringContent(model.Description_ru ?? ""), "Description_ru");
            formData.Add(new StringContent(model.Title_kk ?? ""), "Title_kk");
            formData.Add(new StringContent(model.Description_kk ?? ""), "Description_kk");

            if (model.ImagePath != null)
            {
                using var ms = new MemoryStream();
                await model.ImagePath.CopyToAsync(ms);
                formData.Add(new ByteArrayContent(ms.ToArray())
                {
                    Headers = { ContentType = MediaTypeHeaderValue.Parse(model.ImagePath.ContentType) }
                }, "ImagePath", model.ImagePath.FileName);
            }

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/createBlog", formData);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("BlogList");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        public async Task<IActionResult> EditBlog(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/getById/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("BlogList");

            var json = await response.Content.ReadAsStringAsync();
            var translations = JsonConvert.DeserializeObject<List<BlogTranslationModel>>(json);

            var model = new UpdateBlogViewModel
            {
                Id = id,
                Title_en = translations.FirstOrDefault(t => t.Language == "en-US")?.Title,
                Description_en = translations.FirstOrDefault(t => t.Language == "en-US")?.Description,
                Title_ru = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Title,
                Description_ru = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Description,
                Title_kk = translations.FirstOrDefault(t => t.Language == "kk-KZ")?.Title,
                Description_kk = translations.FirstOrDefault(t => t.Language == "kk-KZ")?.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(UpdateBlogViewModel model)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Title_en ?? ""), "Title_en");
            formData.Add(new StringContent(model.Description_en ?? ""), "Description_en");
            formData.Add(new StringContent(model.Title_ru ?? ""), "Title_ru");
            formData.Add(new StringContent(model.Description_ru ?? ""), "Description_ru");
            formData.Add(new StringContent(model.Title_kk ?? ""), "Title_kk");
            formData.Add(new StringContent(model.Description_kk ?? ""), "Description_kk");

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
                return RedirectToAction("BlogList");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/delete/{id}");
            return RedirectToAction("BlogList");
        }
    }
}
