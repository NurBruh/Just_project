using System.ComponentModel.DataAnnotations;

namespace Just_project.API.Models
{
    public class CreateBlogViewModel
    {
        public string Title_en { get; set; }
        public string Description_en { get; set; }

        public string Title_ru { get; set; }
        public string Description_ru { get; set; }

        public string Title_kk { get; set; }
        public string Description_kk { get; set; }

        [Required(ErrorMessage = "Заливайте изображения!")]
        public IFormFile ImagePath { get; set; }
    }
}
