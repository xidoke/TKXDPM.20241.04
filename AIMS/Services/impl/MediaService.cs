using AIMS.Models.Entities;
using AIMS.Services.impl;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Services
{
    public class MediaService : BaseService<MediaService, Media>, IMediaService
    {
        private readonly IBookService bookService;
        private readonly IDVDService dvdService;
        private readonly ICDService cdService;

        public MediaService()
        {
        }

        public MediaService(IBookService bookService, ICDService cdService, IDVDService dvdService)
        {
            this.bookService = bookService;
            this.cdService = cdService;
            this.dvdService = dvdService;
        }

        public async Task<List<Media>> GetAllMediaAsync()
        {
            return await SelectDataAsync();
        }

        public async Task<Media> GetMediaByIdAsync(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            List<Media> mediaList = await SelectDataAsync(where, parameters);
            return mediaList.Count > 0 ? mediaList[0] : null;
        }

        public async Task UpdateMediaAsync(Media media)
        {
            Dictionary<string, object> setValues = new Dictionary<string, object>
            {
                { "category", media.Category },
                { "type", media.Type },
                { "price", media.Price },
                { "value", media.Value },
                { "quantity", media.Quantity },
                { "title", media.Title },
                { "imgURL", media.ImgURL },
                { "rush_support", media.IsSupportRushShipping },
                { "weight", media.Weight }
            };
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", media.Id }
            };
            await SelectDataAsync(where, parameters);
        }

        public async Task DeleteMediaAsync(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            await DeleteDataAsync(where, parameters);
        }
       
        public async Task AddMediaAsync(Media media, Book book = null, CD cd = null, DVD dvd = null)
        {
            try
            {
                Dictionary<string, object> mediaValues = new Dictionary<string, object>
                {
                    { "category", media.Category },
                    { "type", media.Type },
                    { "price", media.Price },
                    { "value", media.Value },
                    { "quantity", media.Quantity },
                    { "title", media.Title },
                    { "imgURL", media.ImgURL },
                    { "rush_support", media.IsSupportRushShipping },
                    { "weight", media.Weight }
                };
                await InsertDataAsync(mediaValues);

                string whereClause = "category = @category AND type = @type AND title = @title";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "category", media.Category },
                    { "type", media.Type },
                    { "title", media.Title }
                };

                List<Media> insertedMediaList = await SelectDataAsync(whereClause, parameters);

                if (insertedMediaList.Count == 0)
                {
                    throw new Exception("Failed to retrieve the newly inserted Media.");
                }

                int mediaId = insertedMediaList[0].Id; 

                switch (media.Category)
                {
                    case "Book":
                        if (book == null) throw new ArgumentException("Book object cannot be null when category is Book");
                        book.Id = mediaId;
                        await bookService.InsertBookAsync(book);
                        break;

                    case "CD":
                        if (cd == null) throw new ArgumentException("CD object cannot be null when category is CD");
                        cd.Id = mediaId;
                        await cdService.InsertCDAsync(cd);
                        break;

                    case "DVD":
                        if (dvd == null) throw new ArgumentException("DVD object cannot be null when category is DVD");
                        dvd.Id = mediaId;
                        await dvdService.InsertDVDAsync(dvd);
                        break;

                    default:
                        throw new ArgumentException("Invalid media category");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Media>> GetMediaOrderedByTitleAsync(bool ascending)
        {
            return await SelectDataAsync(orderBy: "title", ascending: ascending);
        }

        public async Task<int> GetMediaCountByCategoryAsync(string category)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return await SelectCountAsync(where, parameters);
        }

        public async Task<List<Media>> GetMediaByCategoryOrderedByPriceAsync(string category, bool ascending)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return await SelectDataAsync(where, parameters, "price", ascending);
        }

        public async Task<List<Media>> GetMediasAsync(string where, Dictionary<string, object> parameters = null, 
            string orderBy = null, bool ascending = true)
        {
            return await SelectDataAsync(where, parameters, orderBy, ascending);
        }

        protected override void Map(NpgsqlDataReader reader, Media media)
        {
            media.Id = reader.GetInt32(reader.GetOrdinal("id"));
            media.Category = reader.IsDBNull(reader.GetOrdinal("category")) ? null : reader.GetString(reader.GetOrdinal("category"));
            media.Type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type"));
            media.Price = reader.GetInt32(reader.GetOrdinal("price"));
            media.Value = reader.GetInt32(reader.GetOrdinal("value"));
            media.Quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
            media.Title = reader.IsDBNull(reader.GetOrdinal("title")) ? null : reader.GetString(reader.GetOrdinal("title"));
            media.ImgURL = reader.IsDBNull(reader.GetOrdinal("imgURL")) ? null : reader.GetString(reader.GetOrdinal("imgURL"));
            media.IsSupportRushShipping = reader.GetBoolean(reader.GetOrdinal("rush_support"));
            media.Weight = reader.GetInt32(reader.GetOrdinal("weight"));
        }

        /* public async Task<List<Media>> getListDVD()
        {
            string categoryToSearch = "DVD";
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            return await GetMediasAsync(whereClause, parameters);
        }
        public async Task<List<Media>> getListCD()
        {
            string categoryToSearch = "CD";
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            return await GetMediasAsync(whereClause, parameters);
        }
        public async Task<List<Media>> getListBook()
        {
            string categoryToSearch = "Book";
            string whereClause = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "category", categoryToSearch }
            };
            return await GetMediasAsync(whereClause, parameters);
        }*/
    }
}