namespace Just_project.Models
{
    public class CreateComponentsViewModel
    {
        public string Title_en { get; set; }
        public string Description_en { get; set; }
        public string Title_ru { get; set; }
        public string Description_ru { get; set; }
        public string Title_kk { get; set; }
        public string Description_kk { get; set; }
        public float Price { get; set; }
        public IFormFile ImagePath { get; set; }

    }
}
