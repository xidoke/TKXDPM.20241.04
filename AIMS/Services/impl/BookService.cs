using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services.impl
{
    public class BookService : BaseService<BookService, Book>, IBookService
    {
        public async Task InsertBookAsync(Book book)
        {
            Dictionary<string, object> bookValues = new Dictionary<string, object>
            {
                { "id", book.Id },
                { "author", book.author },
                { "cover_type", book.coverType },
                { "publisher", book.publisher },
                { "publish_date", book.publishDate },
                { "number_of_pages", book.numberOfPages },
                { "language", book.language }
            };
            await InsertDataAsync(bookValues);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", bookId }
            };
            List<Book> bookList = await SelectDataAsync(where, parameters);
            return bookList.Count > 0 ? bookList[0] : null;
        }

        protected override void Map(NpgsqlDataReader reader, Book book)
        {
            book.Id = reader.GetInt32(reader.GetOrdinal("id"));
            book.author = reader.IsDBNull(reader.GetOrdinal("author")) ? null : reader.GetString(reader.GetOrdinal("author"));
            book.coverType = reader.IsDBNull(reader.GetOrdinal("cover_type")) ? null : reader.GetString(reader.GetOrdinal("cover_type"));
            book.publisher = reader.IsDBNull(reader.GetOrdinal("publisher")) ? null : reader.GetString(reader.GetOrdinal("publisher"));
            book.publishDate = reader.IsDBNull(reader.GetOrdinal("publish_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("publish_date"));
            book.numberOfPages = reader.IsDBNull(reader.GetOrdinal("number_of_pages")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("number_of_pages"));
            book.language = reader.IsDBNull(reader.GetOrdinal("language")) ? null : reader.GetString(reader.GetOrdinal("language"));
        }
    }
}
