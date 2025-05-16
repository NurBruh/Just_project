using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Just_project.Admin.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminBlogController(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5199/api/"); // Укажи свой порт
        }

        public async Task<IActionResult> BlogList()
        {
            var response = await _httpClient.GetAsync("BlogAPI/getAll");
            if (!response.IsSuccessStatusCode) return View(new List<BlogTranslationModel>());

            var json = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<BlogTranslationModel>>(json);
            return View(blogs);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(CreateBlogViewModel model)
        {
            var form = new MultipartFormDataContent();
            if (model.ImagePath != null)
            {
                var imageContent = new StreamContent(model.ImagePath.OpenReadStream());
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImagePath.ContentType);
                form.Add(imageContent, "ImagePath", model.ImagePath.FileName);
            }
            form.Add(new StringContent(model.Title_en ?? ""), "Title_en");
            form.Add(new StringContent(model.Description_en ?? ""), "Description_en");
            form.Add(new StringContent(model.Title_ru ?? ""), "Title_ru");
            form.Add(new StringContent(model.Description_ru ?? ""), "Description_ru");
            form.Add(new StringContent(model.Title_kk ?? ""), "Title_kk");
            form.Add(new StringContent(model.Description_kk ?? ""), "Description_kk");

            var response = await _httpClient.PostAsync("BlogAPI/createBlog", form);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"BlogAPI/delete{id}");
            return RedirectToAction("Index");
        }
    }
}
