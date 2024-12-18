using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Models.Entities
{
    public class Province
    {
        public string Id { get; set; }  
        public string Name { get; set; }  

        public List<District> Districts { get; set; } = new List<District>();
    }

}
