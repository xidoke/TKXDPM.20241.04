using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class CD : Media
    {
        public int id { get; set; } 

        [StringLength(45)]
        public string artist { get; set; }

        [StringLength(45)]
        public string recordLabel { get; set; }

        [StringLength(255)]
        public string tracklist { get; set; }

        public DateTime? releaseDate { get; set; }
    }
}