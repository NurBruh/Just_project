namespace Just_project.Admin.Models
{
    public class BlogTranslationModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BlogModel BlogModel { get; set; }
    }
}
