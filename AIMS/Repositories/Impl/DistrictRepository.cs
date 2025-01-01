using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Repositories.Impl
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
        public async Task<District> GetById(string id)
        {
            return await _context.Districts.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
