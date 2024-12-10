using AIMS.Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Services
{
    public class MediaService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

        public async Task<List<Media>> GetAllMediaAsync()
        {
            return await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia);
        }

        public async Task<Media> GetMediaByIdAsync(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            List<Media> mediaList = await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia, where, parameters);
            return mediaList.Count > 0 ? mediaList[0] : null;
        }

        public async Task UpdateMediaAsync(Media media)
        {
            Dictionary<string, object> setValues = new Dictionary<string, object>
            {
                { "category", media.category },
                { "type", media.type },
                { "price", media.price },
                { "quantity", media.quantity },
                { "title", media.title },
                { "imgURL", media.imgURL },
                { "rush_support", media.rush_support }
            };
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", media.id }
            };
            await dbConnect.UpdateDataAsync("Media", setValues, where, parameters);
        }

        public async Task DeleteMediaAsync(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            await dbConnect.DeleteDataAsync("Media", where, parameters);
        }
        public async Task<List<Media>> getListDVD()
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
        }
        public async Task AddMediaAsync(Media media)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "category", media.category },
                { "type", media.type },
                { "price", media.price },
                { "quantity", media.quantity },
                { "title", media.title },
                { "imgURL", media.imgURL },
                { "rush_support", media.rush_support }
            };
            await dbConnect.InsertDataAsync("Media", values);
        }

        public async Task<List<Media>> GetMediaOrderedByTitleAsync(bool ascending)
        {
            return await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia, orderBy: "title", ascending: ascending);
        }

        public async Task<int> GetMediaCountByCategoryAsync(string category)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return await dbConnect.SelectCountAsync("Media", where, parameters);
        }

        public async Task<List<Media>> GetMediaByCategoryOrderedByPriceAsync(string category, bool ascending)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia, where, parameters, "price", ascending);
        }

        public async Task<List<Media>> GetMediasAsync(string where, Dictionary<string, object> parameters = null, string orderBy = null, bool ascending = true)
        {
            return await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia, where, parameters, orderBy, ascending);
        }

        private Media MapDataReaderToMedia(NpgsqlDataReader reader)
        {
            return new Media
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                category = reader.IsDBNull(reader.GetOrdinal("category")) ? null : reader.GetString(reader.GetOrdinal("category")),
                type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type")),
                price = reader.GetInt32(reader.GetOrdinal("price")),
                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                title = reader.IsDBNull(reader.GetOrdinal("title")) ? null : reader.GetString(reader.GetOrdinal("title")),
                imgURL = reader.IsDBNull(reader.GetOrdinal("imgURL")) ? null : reader.GetString(reader.GetOrdinal("imgURL")),
                rush_support = reader.GetBoolean(reader.GetOrdinal("rush_support"))
            };
        }
    }
}