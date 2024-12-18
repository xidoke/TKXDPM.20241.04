using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Models.Entities
{
    public class District
    {
        public string Id { get; set; }  
        public string Name { get; set; }  

        public string ProvinceId { get; set; }  
        public Province Province { get; set; }  

        public List<Ward> Wards { get; set; } = new List<Ward>();
    }

}
