using Just_project.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Just_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PcApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("createPc")]
        public IActionResult CreatePc([FromForm] CreatePcViewModel model)
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

                var pc = new PcModel
                {
                    Price = model.Price,
                    ImagePath = imageData,
                    PcTranslations = new List<PcTranslationModel>
                {
                    new PcTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en },
                    new PcTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru },
                    new PcTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk }
                }
                };

                _db.Pcs.Add(pc);
                _db.SaveChanges();

                return Ok(new { message = "ПК успешно добавлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet("getAll")]
        
        public List<PcTranslationModel> GetAll()
        {
            return _db.PcTranslations.ToList();
        }

        [HttpGet("byPcId/{pcId:int}")]
        public ActionResult<List<PcTranslationModel>> GetByPcId(int pcId)
        {
            var translations = _db.PcTranslations
                .Where(t => t.PcId == pcId)
                .Select(t => new PcTranslationModel
                {
                    Id = t.Id,
                    PcId = t.PcId,
                    Language = t.Language,
                    Title = t.Title,
                    Description = t.Description,
                    PcModel = null 
                })
                .ToList();

            if (!translations.Any())
                return NotFound(new { message = "Переводы для указанного ПК не найдены" });

            return Ok(translations);
        }




        [HttpPut("update")]
        public IActionResult Update([FromForm] UpdatePcViewModel model)
        {
            try
            {
                var pc = _db.Pcs
                    .Include(p => p.PcTranslations)
                    .FirstOrDefault(p => p.Id == model.Id);

                if (pc == null)
                    return NotFound(new { message = "ПК не найден" });

                pc.Price = model.Price;

                
                if (model.NewImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.NewImage.CopyTo(memoryStream);
                        pc.ImagePath = memoryStream.ToArray();
                    }
                }

                
                foreach (var translation in pc.PcTranslations)
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

                _db.SaveChanges();
                return Ok(new { message = "ПК успешно обновлён!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }



        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var pc = _db.Pcs.Find(id);
                if (pc == null)
                    return NotFound(new { message = "ПК не найден" });

                _db.Pcs.Remove(pc);
                _db.SaveChanges();
                return Ok(new { message = "ПК удалён!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}