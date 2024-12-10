using System.ComponentModel.DataAnnotations;

namespace AIMS.Models.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string fullname { get; set; }

        [StringLength(255)]
        public string username { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [StringLength(255)]
        public string salt { get; set; }

        public int admin { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string city { get; set; }
    }
}