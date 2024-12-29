using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;

namespace AIMS.Services.impl
{
    public interface IProvinceService
    {
        Task<List<Province>> GetAllProvincesAsync();
        Task<Province> GetProvinceByNameAsync(string provinceName);
    }
}
