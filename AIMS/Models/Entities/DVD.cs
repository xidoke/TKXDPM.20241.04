using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class DVD : Media
    {
        public int id { get; set; } 

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
    }
}