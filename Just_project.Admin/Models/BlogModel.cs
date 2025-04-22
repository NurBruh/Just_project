namespace Just_project.Admin.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "admin";
        public ICollection<BlogTranslationModel> BlogTranslations { get; set; }
    }
}
