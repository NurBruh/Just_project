namespace Just_project.API.Models
{
    public class UpdateBlogViewModel
    {
        public int Id { get; set; }
        public string Title_en { get; set; }
        public string Description_en { get; set; }

        public string Title_ru { get; set; }
        public string Description_ru { get; set; }

        public string Title_kk { get; set; }
        public string Description_kk { get; set; }

        public IFormFile? NewImage { get; set; }
        
    }
}
