namespace Just_project.Models
{
    public class ComplistModel
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "admin";
        public byte[] ImagePath { get; set; }
        public ICollection<ComplistTranslationModel> ComplistTranslations { get; set; }
    }
}
