using Just_project.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Just_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("createBlog")]
        public IActionResult CreateBlog([FromForm] CreateBlogViewModel model)
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

                var blog = new BlogModel
                {
                    ImagePath = imageData,
                    BlogTranslations = new List<BlogTranslationModel>
            {
                new BlogTranslationModel { Language = "en-US", Title = model.Title_en, Description = model.Description_en },
                new BlogTranslationModel { Language = "ru-RU", Title = model.Title_ru, Description = model.Description_ru },
                new BlogTranslationModel { Language = "kk-KZ", Title = model.Title_kk, Description = model.Description_kk }
            }
                };

                _db.Blogs.Add(blog);
                _db.SaveChanges();

                return Ok(new { message = "Блог успешно добавлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }
        [HttpGet("getAll")]
        public List<BlogTranslationModel> GetAllBlogs()
        {
            return _db.BlogTranslations.ToList();

        }

        [HttpGet("getById/{id:int}")]
        public ActionResult<List<BlogTranslationModel>> GetBlogById(int blogId)
        {
           
                var translations = _db.BlogTranslations
                    .Where(t => t.BlogId == blogId)
                    .Select(t => new BlogTranslationModel
                    {
                        Id = t.Id,
                        BlogId = t.BlogId,
                        Language = t.Language,
                        Title = t.Title,
                        Description = t.Description,
                        BlogModel = null
                    })
                    .ToList();

                if (!translations.Any())
                {
                    return NotFound(new { message = "Переводы для указанного блога не найдены." });
                }

                return Ok(translations);
            
        }
        [HttpPut("update")]
        public IActionResult UpdateBlog([FromForm] UpdateBlogViewModel model )
        {
            try
            {
                var blog = _db.Blogs.Include(b => b.BlogTranslations).FirstOrDefault(b => b.Id == model.Id);
                if (blog == null)
                    return NotFound(new { message = "Блог не найден" });

                if (model.NewImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.NewImage.CopyTo(memoryStream);
                        blog.ImagePath = memoryStream.ToArray();
                    }
                }

                foreach (var translation in blog.BlogTranslations)
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
                return Ok(new { message = "Блог обновлён!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, 
                    inner = ex.InnerException?.Message });
            }
        }

        [HttpDelete("delete{id:int}")]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                var blog = _db.Blogs.Find(id);
                if (blog == null)
                    return NotFound(new { message = "Блог не найден" });
                _db.Blogs.Remove(blog);
                _db.SaveChanges();
                return Ok(new { message = "Блог удалён!" });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, inner = ex.InnerException?.Message });
            }
        }

    }
}
