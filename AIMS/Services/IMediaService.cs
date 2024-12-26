using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIMS.Models.Entities;

namespace AIMS.Services
{
    public interface IMediaService
    {
        Task<List<Media>> GetAllMediaAsync();
        Task<Media> GetMediaByIdAsync(int mediaId);
        Task UpdateMediaAsync(Media media);
        Task DeleteMediaAsync(int mediaId);
        Task AddMediaAsync(Media media, Book book = null, CD cd = null, DVD dvd = null);
        Task<List<Media>> GetMediaOrderedByTitleAsync(bool ascending);
        Task<List<Media>> GetMediaByCategoryOrderedByPriceAsync(string category, bool ascending);
        Task<List<Media>> GetMediasAsync(string where, Dictionary<string, object> parameters = null,
            string orderBy = null, bool ascending = true);
    }
}
