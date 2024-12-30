using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Data.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly ApplicationDbContext _context;

        public DistrictRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<District>> GetDistrictsByProvinceIdAsync(string provinceId)
        {
            return await _context.Districts
                .Where(d => d.ProvinceId == provinceId)
                .ToListAsync();
        }
    }
}
