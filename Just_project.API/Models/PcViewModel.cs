namespace Just_project.API.Models
{
    public class PcViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public float Price { get; set; } 
        public string ShowPrice => $"{Price}  ₸";
        public byte[] ImagePath { get; set; }
    }
}
