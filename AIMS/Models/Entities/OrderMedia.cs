using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class OrderMedia
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("OrderData")]
        public int orderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Media")]
        public int mediaID { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }

    }
}