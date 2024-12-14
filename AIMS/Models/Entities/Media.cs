using System.ComponentModel.DataAnnotations;

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
        [StringLength(45)]
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
        public bool rush_support { get; set; }

        [Required]
        public double weight { get; set; }
        public string getPrice()
        {
            return string.Format("{0:N0}", price);
        }
        public bool isEnough(int quantity)
        {
            return this.quantity >= quantity;
        }
    }
}