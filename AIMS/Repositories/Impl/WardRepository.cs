using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Repositories.Impl
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
        public async Task<Ward> GetById(string id)
        {
            return await _context.Wards.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
