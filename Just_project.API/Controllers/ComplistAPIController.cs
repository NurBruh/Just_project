using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Just_project.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Just_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplistAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ComplistAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("createComplist")]
        public IActionResult CreateComplist([FromForm] CreateComplistViewModel model)
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
                var complist = new ComplistModel
                {
                    ImagePath = imageData,
                    ComplistTranslations = new List<ComplistTranslationModel>
                    {
                        new ComplistTranslationModel { Language = "en-US", Title = model.Title_en },
                        new ComplistTranslationModel { Language = "ru-RU", Title = model.Title_ru },
                        new ComplistTranslationModel { Language = "kk-KZ", Title = model.Title_kk }
                    }
                };
                _db.Complists.Add(complist);
                _db.SaveChanges();
                return Ok(new { message = "Комплектющий ПК успешно добавлена!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet("getAll")]
        public List<ComplistTranslationModel> GetAllComplists()
        {
            return _db.ComplistTranslations.ToList();
        }
        [HttpGet("getById/{id}")]
        public ActionResult<List<ComplistTranslationModel>> GetComplistById(int id) { 
            var translations = _db.ComplistTranslations.Where(t => t.ComplistId == id).Select(t => new ComplistTranslationModel
            {
                Id = t.Id,
                ComplistId = t.ComplistId,
                Language = t.Language,
                Title = t.Title,
                ComplistModel = null

            }).ToList();

            if(!translations.Any())
            {
                return NotFound(new { message = "Комплектующий ПК не найден!" });
            }
            return Ok(translations);

        }
        [HttpPut("update")]
        public IActionResult UpdateComplist([FromForm] UpdateComplistViewModel model)

        {
            try
            {
                var complist = _db.Complists.Include(c => c.ComplistTranslations).FirstOrDefault(c => c.Id == model.Id);
                if (complist == null)
                {
                    return NotFound(new { message = "Комплектующий ПК не найден!" });
                }

                if (model.NewImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.NewImage.CopyTo(memoryStream);
                        complist.ImagePath = memoryStream.ToArray();
                    }
                }

                foreach (var translation in complist.ComplistTranslations)
                {
                    switch (translation.Language)
                    {
                        case "en-US":
                            translation.Title = model.Title_en;
                            break;
                        case "ru-RU":
                            translation.Title = model.Title_ru;
                            break;
                        case "kk-KZ":
                            translation.Title = model.Title_kk;
                            break;
                    }
                }

                _db.SaveChanges();
                return Ok(new { message = "Комплектующий ПК успешно обновлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }
        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteComplist(int id)
        {
            try
            {
                var complist = _db.Complists.Find(id);
                if (complist == null)
                    return NotFound(new { message = "Комплектующий ПК не найден!" });
                _db.Complists.Remove(complist);
                _db.SaveChanges();
                return Ok(new { message = "Комплектующий ПК успешно удален!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }
    }
}
