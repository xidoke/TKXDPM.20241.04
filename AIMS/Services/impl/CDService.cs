using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services.impl
{
    public class CDService : BaseService<CDService, CD>, ICDService
    {
        public async Task InsertCDAsync(CD cd)
        {
            Dictionary<string, object> cdValues = new Dictionary<string, object>
                        {
                            { "id", cd.Id },
                            { "artist", cd.artist },
                            { "record_label", cd.recordLabel },
                            { "tracklist", cd.tracklist },
                            { "release_date", cd.releaseDate }
                        };
            await InsertDataAsync(cdValues);
        }

        public async Task<CD> GetCDByIdAsync(int cdId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", cdId }
            };
            List<CD> cdList = await SelectDataAsync(where, parameters);
            return cdList.Count > 0 ? cdList[0] : null;
        }
        protected override void Map(NpgsqlDataReader reader, CD cd)
        {
            cd.Id = reader.GetInt32(reader.GetOrdinal("id"));
            cd.artist = reader.IsDBNull(reader.GetOrdinal("artist")) ? null : reader.GetString(reader.GetOrdinal("artist"));
            cd.recordLabel = reader.IsDBNull(reader.GetOrdinal("record_label")) 
                ? null : reader.GetString(reader.GetOrdinal("record_label"));
            cd.tracklist = reader.IsDBNull(reader.GetOrdinal("tracklist")) 
                ? null : reader.GetString(reader.GetOrdinal("tracklist"));
            cd.releaseDate = reader.IsDBNull(reader.GetOrdinal("release_date")) 
                ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("release_date"));
        }
    }
}
