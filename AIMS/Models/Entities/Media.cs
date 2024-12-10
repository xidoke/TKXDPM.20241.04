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
        [StringLength(10)]
        public string type { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public int value { get; set; } // Giá trị sản phẩm (chưa VAT)

        [Required]
        public int quantity { get; set; }

        [Required]
        [StringLength(45)]
        public string title { get; set; }

        [StringLength(255)]
        public string imgURL { get; set; }

        [Required]
        public bool rushSupport { get; set; }

        [Required]
        public decimal weight { get; set; } // Dùng decimal cho weight

        [StringLength(255)]
        public string barcode { get; set; }

        [Column(TypeName = "text")] // Sử dụng Column(TypeName = "text") cho kiểu text
        public string productDescription { get; set; }

        [StringLength(255)]
        public string dimension { get; set; }

        public DateTime warehouseEntryDate { get; set; }

        [StringLength(255)]
        public string condition { get; set; }
    }
}