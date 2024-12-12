using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class Media
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string category { get; set; }

        [Required]
        [StringLength(45)] // Cập nhật độ dài
        public string type { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [StringLength(255)]
        public string imgURL { get; set; }

        [Required]
        public bool rush_support { get; set; } // Đổi tên thuộc tính
        public string getPrice()
        {
            return string.Format("{0:N0}", price);
        }
    }
}