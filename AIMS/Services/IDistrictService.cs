using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIMS.Models.Entities;

namespace AIMS.Services
{
    public interface IDistrictService 
    {
        Task<List<District>> GetDistrictsByProvinceAsync(string provinceId);
        Task<District> GetDistrictByNameAsync(string districtName);
    }
}
