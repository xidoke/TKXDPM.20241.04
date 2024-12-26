using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services.impl
{
    public class DVDService : BaseService<DVDService, DVD>, IDVDService
    {
        public async Task InsertDVDAsync(DVD dvd)
        {
            Dictionary<string, object> dvdValues = new Dictionary<string, object>
            {
                { "id", dvd.Id },
                { "disc_type", dvd.discType },
                { "director", dvd.director },
                { "runtime", dvd.runtime },
                { "studio", dvd.studio },
                { "subtitle", dvd.subtitle },
                { "release_date", dvd.releaseDate }
            };
            await InsertDataAsync(dvdValues);
        }

        public async Task<DVD> GetDVDByIdAsync(int dvdId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", dvdId }
            };
            List<DVD> dvdList = await SelectDataAsync(where, parameters);
            return dvdList.Count > 0 ? dvdList[0] : null;
        }

        protected override void Map(NpgsqlDataReader reader, DVD dvd)
        {
            dvd.Id = reader.GetInt32(reader.GetOrdinal("id"));
            dvd.discType = reader.IsDBNull(reader.GetOrdinal("disc_type")) ? null : reader.GetString(reader.GetOrdinal("disc_type"));
            dvd.director = reader.IsDBNull(reader.GetOrdinal("director")) ? null : reader.GetString(reader.GetOrdinal("director"));
            dvd.runtime = reader.IsDBNull(reader.GetOrdinal("runtime")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("runtime"));
            dvd.studio = reader.IsDBNull(reader.GetOrdinal("studio")) ? null : reader.GetString(reader.GetOrdinal("studio"));
            dvd.subtitle = reader.IsDBNull(reader.GetOrdinal("subtitle")) ? null : reader.GetString(reader.GetOrdinal("subtitle"));
            dvd.releaseDate = reader.IsDBNull(reader.GetOrdinal("release_date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("release_date"));
        }
    }
}
