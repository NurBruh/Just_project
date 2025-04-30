namespace Just_project.API.Models
{
    public class ComponentsTranslationModel
    {
        public int Id { get; set; }
        public int ComponentsId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ComponentsModel ComponentsModel { get; set; }
    }
}
