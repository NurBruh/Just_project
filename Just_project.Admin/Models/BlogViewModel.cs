namespace Just_project.Admin.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string Date => CreateTime.ToShortDateString();
        public byte[] ImagePath { get; set; }
    }
}
