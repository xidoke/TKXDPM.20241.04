using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Repositories.Impl
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDbContext _context;

        public ProvinceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Province>> GetAllAsync()
        {
            return await _context.Provinces.ToListAsync();
        }
        public async Task<Province> GetById(string id)
        {
            return await _context.Provinces.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
