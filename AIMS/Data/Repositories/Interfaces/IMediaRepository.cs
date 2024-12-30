using AIMS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IMediaRepository
    {
        Task<List<Media>> GetAllAsync();
        Task<Media> GetByIdAsync(int id);
        Task<List<Media>> SearchAsync(string keyword = null, string category = null);
        Task AddAsync(Media media);
        Task UpdateAsync(Media media);
        Task DeleteAsync(int id);
        Task<List<CD>> GetCDsAsync();
        Task<List<DVD>> GetDVDsAsync();
        Task<List<Book>> GetBooksAsync();
    }
}