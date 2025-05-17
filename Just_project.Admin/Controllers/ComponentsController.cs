using Just_project.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

public class ComponentsController : Controller
{
    private readonly HttpClient _http;

    public ComponentsController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient();
        _http.BaseAddress = new Uri("http://localhost:5199/api/ComponentsAPI/");
    }

    public async Task<IActionResult> ComponentsList()
    {
        var response = await _http.GetAsync("getAll");
        if (!response.IsSuccessStatusCode)
            return View(new List<ComponentsViewModel>());

        var json = await response.Content.ReadAsStringAsync();
        var translations = JsonConvert.DeserializeObject<List<ComponentsTranslationModel>>(json);

        // Группируем переводы по ID компонента
        var componentsViewModels = new List<ComponentsViewModel>();
        var groupedTranslations = translations
            .GroupBy(t => t.ComponentsId)
            .ToList();

        foreach (var group in groupedTranslations)
        {
            // Получаем детальную информацию о компоненте
            var detailResponse = await _http.GetAsync($"getById/{group.Key}");
            if (detailResponse.IsSuccessStatusCode)
            {
                var detailJson = await detailResponse.Content.ReadAsStringAsync();
                var componentTranslations = JsonConvert.DeserializeObject<List<ComponentsTranslationModel>>(detailJson);

                // Создаем ViewModel на основе доступных данных
                var componentViewModel = new ComponentsViewModel
                {
                    Id = group.Key,
                    Title = componentTranslations.FirstOrDefault(t => t.Language == "ru-RU")?.Title ?? "",
                    Description = componentTranslations.FirstOrDefault(t => t.Language == "ru-RU")?.Description ?? "",
                    // Примечание: цена и изображение из API не доступны напрямую,
                    // поэтому оставляем значения по умолчанию
                    Price = 0,
                    ImagePath = null
                };

                componentsViewModels.Add(componentViewModel);
            }
        }

        return View(componentsViewModels);
    }

    public IActionResult ComponentsAdd()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ComponentsAdd(CreateComponentsViewModel model)
    {
        var form = new MultipartFormDataContent();

        // Проверяем наличие данных перед добавлением в форму
        if (!string.IsNullOrEmpty(model.Title_en))
            form.Add(new StringContent(model.Title_en), "Title_en");
        else
            form.Add(new StringContent(""), "Title_en");

        if (!string.IsNullOrEmpty(model.Description_en))
            form.Add(new StringContent(model.Description_en), "Description_en");
        else
            form.Add(new StringContent(""), "Description_en");

        if (!string.IsNullOrEmpty(model.Title_ru))
            form.Add(new StringContent(model.Title_ru), "Title_ru");
        else
            form.Add(new StringContent(""), "Title_ru");

        if (!string.IsNullOrEmpty(model.Description_ru))
            form.Add(new StringContent(model.Description_ru), "Description_ru");
        else
            form.Add(new StringContent(""), "Description_ru");

        if (!string.IsNullOrEmpty(model.Title_kk))
            form.Add(new StringContent(model.Title_kk), "Title_kk");
        else
            form.Add(new StringContent(""), "Title_kk");

        if (!string.IsNullOrEmpty(model.Description_kk))
            form.Add(new StringContent(model.Description_kk), "Description_kk");
        else
            form.Add(new StringContent(""), "Description_kk");

        form.Add(new StringContent(model.Price.ToString()), "Price");

        if (model.ImagePath != null)
        {
            using (var stream = model.ImagePath.OpenReadStream())
            {
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImagePath.ContentType);
                form.Add(fileContent, "ImagePath", model.ImagePath.FileName);
            }
        }

        var response = await _http.PostAsync("createComponent", form);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("ComponentsList");

        var errorContent = await response.Content.ReadAsStringAsync();
        ViewBag.Error = errorContent;
        return View(model);
    }

    public async Task<IActionResult> ComponentsEdit(int id)
    {
        try
        {
            var response = await _http.GetAsync($"getById/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var translations = JsonConvert.DeserializeObject<List<ComponentsTranslationModel>>(json);

            if (translations == null || !translations.Any())
                return NotFound("Переводы для компонента не найдены");

            // Извлекаем цену из первой записи, если она есть
            float price = 0;
            try
            {
                if (translations.Any() && translations[0].ComponentsModel != null)
                {
                    price = translations[0].ComponentsModel.Price;
                }
            }
            catch
            {
                // Если не удалось получить цену, используем значение по умолчанию
            }

            var model = new UpdateComponentsViewModel
            {
                Id = id,
                Title_en = translations.FirstOrDefault(t => t.Language == "en-US")?.Title ?? "",
                Description_en = translations.FirstOrDefault(t => t.Language == "en-US")?.Description ?? "",
                Title_ru = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Title ?? "",
                Description_ru = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Description ?? "",
                Title_kk = translations.FirstOrDefault(t => t.Language == "kk-KZ")?.Title ?? "",
                Description_kk = translations.FirstOrDefault(t => t.Language == "kk-KZ")?.Description ?? "",
                Price = price
            };

            return View(model);
        }
        catch (Exception ex)
        {
            return View("Error", new { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ComponentsEdit(UpdateComponentsViewModel model)
    {
        try
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(model.Id.ToString()), "Id" },
                { new StringContent(model.Title_en ?? ""), "Title_en" },
                { new StringContent(model.Description_en ?? ""), "Description_en" },
                { new StringContent(model.Title_ru ?? ""), "Title_ru" },
                { new StringContent(model.Description_ru ?? ""), "Description_ru" },
                { new StringContent(model.Title_kk ?? ""), "Title_kk" },
                { new StringContent(model.Description_kk ?? ""), "Description_kk" },
                { new StringContent(model.Price.ToString()), "Price" }
            };

            if (model.NewImage != null)
            {
                using (var stream = model.NewImage.OpenReadStream())
                {
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.NewImage.ContentType);
                    form.Add(fileContent, "NewImage", model.NewImage.FileName);
                }
            }

            var response = await _http.PutAsync("update", form);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("ComponentsList");

            var errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.Error = errorContent;
            return View(model);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(model);
        }
    }

    public async Task<IActionResult> ComponentsDelete(int id)
    {
        try
        {
            var response = await _http.GetAsync($"getById/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var translations = JsonConvert.DeserializeObject<List<ComponentsTranslationModel>>(json);

            if (translations == null || !translations.Any())
                return NotFound("Переводы для компонента не найдены");

            // Извлекаем цену и изображение, если возможно
            float price = 0;
            byte[] imagePath = null;
            try
            {
                if (translations.Any() && translations[0].ComponentsModel != null)
                {
                    price = translations[0].ComponentsModel.Price;
                    imagePath = translations[0].ComponentsModel.ImagePath;
                }
            }
            catch
            {
                // Если не удалось получить данные, используем значения по умолчанию
            }

            var model = new ComponentsViewModel
            {
                Id = id,
                Title = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Title ?? "",
                Description = translations.FirstOrDefault(t => t.Language == "ru-RU")?.Description ?? "",
                Price = price,
                ImagePath = imagePath
            };

            return View(model);
        }
        catch (Exception ex)
        {
            return View("Error", new { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"delete/{id}");
            return RedirectToAction("ComponentsList");
        }
        catch (Exception)
        {
            // Даже в случае ошибки перенаправляем на список компонентов
            return RedirectToAction("ComponentsList");
        }
    }
}