namespace Just_project.Models
{
    public class ComponentsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string KZTPrice => $"{Price}  ₸";
    }
}
