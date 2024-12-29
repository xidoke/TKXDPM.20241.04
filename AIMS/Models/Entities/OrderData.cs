using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIMS.Models.Entities
{
    public class OrderData
    {
        [Key]
        public int id { get; set; }
        public string fullname { get; set; }

        [StringLength(45)]
        public string province { get; set; }

        [StringLength(45)]
        public string address { get; set; }

        [StringLength(45)]
        public string phone { get; set; }

        [ForeignKey("User")]
        public int? userID { get; set; }
        [ForeignKey("userDeviceID")]
        public int? userDeviceID { get; set; }

        public int shippingFee { get; set; }

        public DateTime createAt { get; set; }

        [StringLength(255)]
        public string instructions { get; set; }

        [StringLength(45)]
        public string type { get; set; }

        public int totalPrice { get; set; }

        [StringLength(45)]
        public string status { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderMedia> OrderMedias { get; set; }
    }
}