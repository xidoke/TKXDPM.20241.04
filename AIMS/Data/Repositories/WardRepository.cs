using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Data.Repositories
{
    public class WardRepository : IWardRepository
    {
        private readonly ApplicationDbContext _context;

        public WardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ward>> GetWardsByDistrictIdAsync(string districtId)
        {
            return await _context.Wards
                .Where(w => w.DistrictId == districtId)
                .ToListAsync();
        }
    }
}
