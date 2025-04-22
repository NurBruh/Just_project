namespace Just_project.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string Date => CreateTime.ToString("dd MMMM yyyy");
    }
}
