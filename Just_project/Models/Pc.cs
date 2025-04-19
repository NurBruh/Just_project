using System.Collections.Generic;

namespace Just_project.Models
{
    public class Pc
    {
        public int Id { get; set; }
        public float Price { get; set; }

        public ICollection<PcTranslation> Translations { get; set; }
    }
}
