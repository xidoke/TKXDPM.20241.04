using System.Collections.Generic;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using AIMS.Services.impl;
using Npgsql;

namespace AIMS.Services
{
    public class WardService : BaseService<WardService, Ward>, IWardService
    {
        public async Task<List<Ward>> GetWardsByDistrictAsync(string districtId)
        {
            string where = "district_id = @district_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "district_id", districtId }
        };
            return await SelectDataAsync(where, parameters);
        }

        protected override void Map(NpgsqlDataReader reader, Ward ward)
        {
            ward.Id = reader.GetString(reader.GetOrdinal("id"));
            ward.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
            ward.DistrictId = reader.IsDBNull(reader.GetOrdinal("district_id")) ? null : reader.GetString(reader.GetOrdinal("district_id"));
        }
    }

}
