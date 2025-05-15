using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Just_project.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Just_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ComponentsAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("createComponent")]
        public IActionResult CreateComponent([FromForm] CreateComponentsViewModel model)
        {
            try
            {
                byte[] imageData = null;
                if (model.ImagePath != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.ImagePath.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }

                var component = new ComponentsModel
                {
                    Price = model.Price,
                    ImagePath = imageData,
                    ComponentsTranslations = new List<ComponentsTranslationModel>
                    {
                        new ComponentsTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en },
                        new ComponentsTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru },
                        new ComponentsTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk }
                    }
                };

                _db.Components.Add(component);
                _db.SaveChanges();
                return Ok(new { message = "Компонент успешно добавлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet("getAll")]
        public List<ComponentsTranslationModel> GetAllComponents()
        {
            return _db.ComponentsTranslations.ToList();
        }

        [HttpGet("getById/{id}")]
        public ActionResult<List<ComponentsTranslationModel>> GetComponentById(int id)
        {
            var translations = _db.ComponentsTranslations.Where(t => t.ComponentsId == id).Select(t => new ComponentsTranslationModel
            {
                Id = t.Id,
                ComponentsId = t.ComponentsId,
                Language = t.Language,
                Title = t.Title,
                Description = t.Description,
                ComponentsModel = null
            }).ToList();

            if (!translations.Any())
            {
                return NotFound(new { message = "Компонент не найден!" });
            }
            return Ok(translations);
        }

        [HttpPut("update")]
        public IActionResult UpdateComponent([FromForm] UpdateComponentsViewModel model)
        {
            try
            {
                var component = _db.Components.Include(c => c.ComponentsTranslations).FirstOrDefault(c => c.Id == model.Id);
                if (component == null)
                {
                    return NotFound(new { message = "Компонент не найден!" });
                }

                if (model.NewImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.NewImage.CopyTo(memoryStream);
                        component.ImagePath = memoryStream.ToArray();
                    }
                }

                foreach (var translation in component.ComponentsTranslations)
                {
                    switch (translation.Language)
                    {
                        case "en-US":
                            translation.Title = model.Title_en;
                            translation.Description = model.Description_en;
                            break;
                        case "ru-RU":
                            translation.Title = model.Title_ru;
                            translation.Description = model.Description_ru;
                            break;
                        case "kk-KZ":
                            translation.Title = model.Title_kk;
                            translation.Description = model.Description_kk;
                            break;
                    }
                }

                component.Price = model.Price;
                _db.SaveChanges();
                return Ok(new { message = "Компонент успешно обновлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteComponent(int id)
        {
            try
            {
                var component = _db.Components.Find(id);
                if (component == null)
                    return NotFound(new { message = "Компонент не найден!" });

                _db.Components.Remove(component);
                _db.SaveChanges();
                return Ok(new { message = "Компонент успешно удален!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }
    }
}
