using System;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Models.Entities
{
    public class DVD : Media
    {

        [StringLength(45)]
        public string discType { get; set; }

        [StringLength(45)]
        public string director { get; set; }

        public int? runtime { get; set; }

        [StringLength(45)]
        public string studio { get; set; }

        [StringLength(45)]
        public string subtitle { get; set; }

        public DateTime? releaseDate { get; set; }
        public string getReleasedDate()
        {
            return releaseDate?.ToString("dd/MM/yyyy");
        }
    }
}