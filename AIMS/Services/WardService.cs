using System.Collections.Generic;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services
{
    public class WardService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

        public async Task<List<Ward>> GetWardsByDistrictAsync(string districtId)
        {
            string where = "district_id = @district_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "district_id", districtId }
        };
            return await dbConnect.SelectDataAsync<Ward>("Wards", MapDataReaderToWard, where, parameters);
        }

        private Ward MapDataReaderToWard(NpgsqlDataReader reader)
        {
            return new Ward
            {
                Id = reader.GetString(reader.GetOrdinal("id")),
                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                DistrictId = reader.IsDBNull(reader.GetOrdinal("district_id")) ? null : reader.GetString(reader.GetOrdinal("district_id"))
            };
        }
    }

}
