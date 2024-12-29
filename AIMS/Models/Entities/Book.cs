using System;
using System.ComponentModel.DataAnnotations;
using Npgsql;

namespace AIMS.Models.Entities
{
    public class Book : Media
    {

        [StringLength(255)]
        public string author { get; set; }

        [StringLength(255)]
        public string coverType { get; set; }

        [StringLength(255)]
        public string publisher { get; set; }

        public DateTime? publishDate { get; set; }

        public int? numberOfPages { get; set; }

        [StringLength(255)]
        public string language { get; set; }
        public string getPublishedDate()
        {
            return publishDate?.ToString("dd/MM/yyyy");
        }
    }
}