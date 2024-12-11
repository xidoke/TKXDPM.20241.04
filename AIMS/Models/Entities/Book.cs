using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class Book : Media
    {
        public int id { get; set; } 

        [StringLength(45)]
        public string author { get; set; }

        [StringLength(45)]
        public string coverType { get; set; }

        [StringLength(45)]
        public string publisher { get; set; }

        public DateTime? publishDate { get; set; }

        public int? numberOfPages { get; set; }

        [StringLength(45)]
        public string language { get; set; }
    }
}