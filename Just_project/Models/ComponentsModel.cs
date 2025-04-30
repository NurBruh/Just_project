namespace Just_project.Models
{
    public class ComponentsModel
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "admin";
        public float Price { get; set; }
        public byte[] ImagePath { get; set; }
        public ICollection<ComponentsTranslationModel> ComponentsTranslations { get; set; }
    }
}
