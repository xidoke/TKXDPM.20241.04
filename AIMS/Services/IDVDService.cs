using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;

namespace AIMS.Services
{
    public interface IDVDService
    {
        Task InsertDVDAsync(DVD dvd);
        Task<DVD> GetDVDByIdAsync(int dvdId);
    }
}
