namespace Just_project.API.Models
{
    public class ComplistTranslationModel
    {
        public int Id { get; set; }
        public int ComplistId { get; set; }
        public string Language { get; set; } 
        public string Title { get; set; }
        public ComplistModel ComplistModel { get; set; }
    }
}
