using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        // GET: AdminBlog/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"BlogAPI/getById/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogViewModel>(json);
            return View(blog);
        }

        // POST: AdminBlog/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"BlogAPI/update/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(BlogList));

            ModelState.AddModelError("", "Ошибка при обновлении записи.");
            return View(model);
        }
        // GET: AdminBlog/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"BlogAPI/getById/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogViewModel>(json);
            return View(blog);
        }

        // POST: AdminBlog/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"BlogAPI/delete/{id}");
            return RedirectToAction(nameof(BlogList));
        }


        
    }
}
