using System.Collections.Generic;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services
{
    public class DistrictService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

        public async Task<List<District>> GetDistrictsByProvinceAsync(string provinceId)
        {
            string where = "province_id = @province_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "province_id", provinceId }
        };
            return await dbConnect.SelectDataAsync<District>("Districts", MapDataReaderToDistrict, where, parameters);
        }

        public async Task<District> GetDistrictByNameAsync(string districtName)
        {
            string where = "name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "name", districtName }
        };
            List<District> districtList = await dbConnect.SelectDataAsync<District>("Districts", MapDataReaderToDistrict, where, parameters);
            return districtList.Count > 0 ? districtList[0] : null;
        }

        public async Task<District> GetDistrictByIdAsync(string districtId)
        {
            string where = "id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "id", districtId }
            };
            List<District> districtList = await dbConnect.SelectDataAsync<District>("Districts", MapDataReaderToDistrict, where, parameters);
            return districtList.Count > 0 ? districtList[0] : null;
        }

        private District MapDataReaderToDistrict(NpgsqlDataReader reader)
        {
            return new District
            {
                Id = reader.GetString(reader.GetOrdinal("id")),
                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                ProvinceId = reader.IsDBNull(reader.GetOrdinal("province_id")) ? null : reader.GetString(reader.GetOrdinal("province_id"))
            };
        }
    }

}
