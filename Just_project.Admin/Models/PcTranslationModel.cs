using System.Collections.Generic;
namespace Just_project.Admin.Models

{
    public class PcTranslationModel
    {
        public int Id { get; set; }

        public int PcId { get; set; }
        

        public string Language { get; set; } // "en-US", "ru-RU", "kk-KZ"
        public string Title { get; set; }
        public string Description { get; set; }

        public PcModel PcModel { get; set; }
    }
}
