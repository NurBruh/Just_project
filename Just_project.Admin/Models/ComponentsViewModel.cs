namespace Just_project.Admin.Models
{
    public class ComponentsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string KZTPrice => $"{Price}  ₸";
        public byte[] ImagePath { get; set; }
    }
}
