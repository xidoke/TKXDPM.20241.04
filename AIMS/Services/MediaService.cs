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
        public async Task AddMediaAsync(Media media, Book book = null, CD cd = null, DVD dvd = null)
        {
            try
            {
                Dictionary<string, object> mediaValues = new Dictionary<string, object>
        {
            { "category", media.category },
            { "type", media.type },
            { "price", media.price },
            { "quantity", media.quantity },
            { "title", media.title },
            { "imgURL", media.imgURL },
            { "rush_support", media.rush_support }
        };
                await dbConnect.InsertDataAsync("Media", mediaValues);

                string whereClause = "category = @category AND type = @type AND title = @title";
                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "category", media.category },
            { "type", media.type },
            { "title", media.title }
        };

                List<Media> insertedMediaList = await dbConnect.SelectDataAsync<Media>("Media", MapDataReaderToMedia, whereClause, parameters);

                if (insertedMediaList.Count == 0)
                {
                    throw new Exception("Failed to retrieve the newly inserted Media.");
                }

                int mediaId = insertedMediaList[0].id; 

                switch (media.category)
                {
                    case "Book":
                        if (book == null) throw new ArgumentException("Book object cannot be null when category is Book");
                        book.id = mediaId;
                        Dictionary<string, object> bookValues = new Dictionary<string, object>
                {
                    { "id", book.id },
                    { "author", book.author },
                    { "cover_type", book.coverType },
                    { "publisher", book.publisher },
                    { "publish_date", book.publishDate },
                    { "number_of_pages", book.numberOfPages },
                    { "language", book.language }
                };
                        await dbConnect.InsertDataAsync("Book", bookValues);
                        break;

                    case "CD":
                        if (cd == null) throw new ArgumentException("CD object cannot be null when category is CD");
                        cd.id = mediaId;
                        Dictionary<string, object> cdValues = new Dictionary<string, object>
                {
                    { "id", cd.id },
                    { "artist", cd.artist },
                    { "record_label", cd.recordLabel },
                    { "tracklist", cd.tracklist },
                    { "release_date", cd.releaseDate }
                };
                        await dbConnect.InsertDataAsync("CD", cdValues);
                        break;

                    case "DVD":
                        if (dvd == null) throw new ArgumentException("DVD object cannot be null when category is DVD");
                        dvd.id = mediaId;
                        Dictionary<string, object> dvdValues = new Dictionary<string, object>
                {
                    { "id", dvd.id },
                    { "disc_type", dvd.discType },
                    { "director", dvd.director },
                    { "runtime", dvd.runtime },
                    { "studio", dvd.studio },
                    { "subtitle", dvd.subtitle },
                    { "release_date", dvd.releaseDate }
                };
                        await dbConnect.InsertDataAsync("DVD", dvdValues);
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
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "id", bookId }
    };
            List<Book> bookList = await dbConnect.SelectDataAsync<Book>("Book", MapDataReaderToBook, where, parameters);
            return bookList.Count > 0 ? bookList[0] : null;
        }

        private Book MapDataReaderToBook(NpgsqlDataReader reader)
        {
            return new Book
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                author = reader.IsDBNull(reader.GetOrdinal("author")) ? null : reader.GetString(reader.GetOrdinal("author")),
                coverType = reader.IsDBNull(reader.GetOrdinal("cover_type")) ? null : reader.GetString(reader.GetOrdinal("cover_type")),
                publisher = reader.IsDBNull(reader.GetOrdinal("publisher")) ? null : reader.GetString(reader.GetOrdinal("publisher")),
                publishDate = reader.IsDBNull(reader.GetOrdinal("publish_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("publish_date")),
                numberOfPages = reader.IsDBNull(reader.GetOrdinal("number_of_pages")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("number_of_pages")),
                language = reader.IsDBNull(reader.GetOrdinal("language")) ? null : reader.GetString(reader.GetOrdinal("language"))
            };
        }
        public async Task<CD> GetCDByIdAsync(int cdId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "id", cdId }
    };
            List<CD> cdList = await dbConnect.SelectDataAsync<CD>("CD", MapDataReaderToCD, where, parameters);
            return cdList.Count > 0 ? cdList[0] : null;
        }

        private CD MapDataReaderToCD(NpgsqlDataReader reader)
        {
            return new CD
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                artist = reader.IsDBNull(reader.GetOrdinal("artist")) ? null : reader.GetString(reader.GetOrdinal("artist")),
                recordLabel = reader.IsDBNull(reader.GetOrdinal("record_label")) ? null : reader.GetString(reader.GetOrdinal("record_label")),
                tracklist = reader.IsDBNull(reader.GetOrdinal("tracklist")) ? null : reader.GetString(reader.GetOrdinal("tracklist")),
                releaseDate = reader.IsDBNull(reader.GetOrdinal("release_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("release_date"))
            };
        }
        public async Task<DVD> GetDVDByIdAsync(int dvdId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "id", dvdId }
    };
            List<DVD> dvdList = await dbConnect.SelectDataAsync<DVD>("DVD", MapDataReaderToDVD, where, parameters);
            return dvdList.Count > 0 ? dvdList[0] : null;
        }

        private DVD MapDataReaderToDVD(NpgsqlDataReader reader)
        {
            return new DVD
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                discType = reader.IsDBNull(reader.GetOrdinal("disc_type")) ? null : reader.GetString(reader.GetOrdinal("disc_type")),
                director = reader.IsDBNull(reader.GetOrdinal("director")) ? null : reader.GetString(reader.GetOrdinal("director")),
                runtime = reader.IsDBNull(reader.GetOrdinal("runtime")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("runtime")),
                studio = reader.IsDBNull(reader.GetOrdinal("studio")) ? null : reader.GetString(reader.GetOrdinal("studio")),
                subtitle = reader.IsDBNull(reader.GetOrdinal("subtitle")) ? null : reader.GetString(reader.GetOrdinal("subtitle")),
                releaseDate = reader.IsDBNull(reader.GetOrdinal("release_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("release_date"))
            };
        }
    }
}