using AIMS.Data.Contexts;
using AIMS.Data.Entities;
using AIMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.Repositories.Impl
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Media>> GetAllAsync()
        {
            return await _context.Medias.ToListAsync();
        }

        public async Task<Media> GetByIdAsync(int id)
        {
            return await _context.Medias.FindAsync(id);
        }

        public async Task<List<Media>> SearchAsync(string keyword, string category = null)
        {
            var query = _context.Medias.AsQueryable();

            if (!string.IsNullOrEmpty(keyword)) query = query.Where(m => m.Title.ToLower().Contains(keyword.ToLower()));
            if (!string.IsNullOrEmpty(category)) query = query.Where(m => m.Category == category);
            return await query.ToListAsync();
        }
        public async Task AddAsync(Media media)
        {
            _context.Medias.Add(media);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Media media)
        {
            _context.Entry(media).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var media = await _context.Medias.FindAsync(id);
            if (media != null)
            {
                _context.Medias.Remove(media);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CD>> GetCDsAsync()
        {
            return await _context.CDs.ToListAsync();
        }

        public async Task<List<DVD>> GetDVDsAsync()
        {
            return await _context.DVDs.ToListAsync();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
    }
}