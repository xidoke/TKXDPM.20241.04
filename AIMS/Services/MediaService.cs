using AIMS.Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace AIMS.Services
{
    public class MediaService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

        public List<Media> GetAllMedia()
        {
            return dbConnect.SelectData("Media", MapDataReaderToMedia);
        }
        public Media GetMediaById(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            List<Media> mediaList = dbConnect.SelectData("Media", MapDataReaderToMedia, where, parameters);
            return mediaList.Count > 0 ? mediaList[0] : null;
        }
        /*
         * 
         * Hướng dẫn sử dụng Update 
        MediaService mediaService = new MediaService();
        int mediaIdToUpdate = 2;
        Media mediaToUpdate = mediaService.GetMediaById(mediaIdToUpdate); 

        if (mediaToUpdate != null)
        {
            mediaToUpdate.price = 120;
            mediaToUpdate.quantity = 50;
            mediaService.UpdateMedia(mediaToUpdate);
        }
        else
        {
            Console.WriteLine($"Media with ID {mediaIdToUpdate} not found.");
        }*/
        public void UpdateMedia(Media media)
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
            dbConnect.UpdateData("Media", setValues, where, parameters);
        }
        public void DeleteMedia(int mediaId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", mediaId }
            };
            dbConnect.DeleteData("Media", where, parameters);
        }
        public void AddMedia(Media media)
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
            dbConnect.InsertData("Media", values);
        }
        public List<Media> GetMediaOrderedByTitle(bool ascending)
        {
            return dbConnect.SelectData("Media", MapDataReaderToMedia, orderBy: "title", ascending: ascending);
        }
        public int GetMediaCountByCategory(string category)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return dbConnect.SelectCount("Media", where, parameters);
        }
        public List<Media> GetMediaByCategoryOrderedByPrice(string category, bool ascending)
        {
            string where = "category = @category";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "category", category }
            };
            return dbConnect.SelectData("Media", MapDataReaderToMedia, where, parameters, "price", ascending);
        }
        public List<Media> GetMedias(string where, Dictionary<string, object> parameters = null, string orderBy = null, bool ascending = true)
        {
            return dbConnect.SelectData("Media", MapDataReaderToMedia, where, parameters, orderBy, ascending);
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