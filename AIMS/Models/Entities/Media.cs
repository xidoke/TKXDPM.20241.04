using System.ComponentModel.DataAnnotations;

namespace AIMS.Models.Entities
{
    public class Media
    {
        [Key]
        protected int id;

        [Required]
        [StringLength(45)]
        protected string category;

        [Required]
        [StringLength(45)]
        protected string type;

        [Required]
        protected int price;

        [Required]
        protected int quantity;

        [Required]
        [StringLength(255)]
        protected string title;

        [StringLength(255)]
        protected string imgURL;

        [Required]
        protected bool rush_support;

        [Required]
        protected double weight;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string ImgURL
        {
            get { return imgURL; }
            set { imgURL = value; }
        }

        public bool RushSupport
        {
            get { return rush_support; }
            set { rush_support = value; }
        }

        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string getPriceFormat()
        {
            return string.Format("{0:N0}", price);
        }

        public bool isEnough(int quantity)
        {
            return this.quantity >= quantity;
        }
    }
}