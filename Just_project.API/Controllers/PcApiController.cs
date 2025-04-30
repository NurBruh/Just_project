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
        public IActionResult GetAll()
        {
            var pcs = _db.Pcs.Include(p => p.PcTranslations).ToList();
            return Ok(pcs);
        }

        [HttpGet("getById/{id:int}")]
        public IActionResult GetById(int id)
        {
            var pc = _db.Pcs.Include(p => p.PcTranslations).FirstOrDefault(p => p.Id == id);
            if (pc == null)
                return NotFound(new { message = "ПК не найден" });

            return Ok(pc);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] PcModel model)
        {
            try
            {
                var pc = _db.Pcs.Include(p => p.PcTranslations).FirstOrDefault(p => p.Id == model.Id);
                if (pc == null)
                    return NotFound(new { message = "ПК не найден" });

                pc.Price = model.Price;
                // Здесь можно обновить переводы, если нужно

                _db.SaveChanges();
                return Ok(new { message = "ПК обновлён!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
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
