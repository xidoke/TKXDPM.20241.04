using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;

namespace AIMS.Services
{
    public interface IWardService
    {
        Task<List<Ward>> GetWardsByDistrictAsync(string districtId);
    }
}
