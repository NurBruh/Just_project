﻿using System.Collections.Generic;

namespace Just_project.API.Models
{
    public class PcModel
    {
        public int Id { get; set; }
        public float Price { get; set; }
        
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "admin";
        public byte[] ImagePath { get; set; }
        public ICollection<PcTranslationModel> PcTranslations { get; set; }
    }
}
