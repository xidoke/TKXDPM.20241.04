using System;
using System.ComponentModel.DataAnnotations;
using Npgsql;
using System.Security.Policy;

namespace AIMS.Models.Entities
{
    public class CD : Media
    {
        [StringLength(45)]
        public string artist { get; set; }

        [StringLength(45)]
        public string recordLabel { get; set; }

        [StringLength(255)]
        public string tracklist { get; set; }

        public DateTime? releaseDate { get; set; }
        public string getReleasedDate()
        {
            return releaseDate?.ToString("dd/MM/yyyy");
        }
    }
}