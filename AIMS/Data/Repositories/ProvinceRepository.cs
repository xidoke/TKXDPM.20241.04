﻿using AIMS.Data.Contexts;
using AIMS.Data.Entities.Address;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Data.Repositories
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
    }
}
