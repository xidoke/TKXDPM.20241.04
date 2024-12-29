using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMS.Models.Entities;
using Npgsql;

namespace AIMS.Services.impl
{
    public class DistrictService : BaseService<DistrictService, District>, IDistrictService
    {
        public async Task<District> GetDistrictByNameAsync(string districtName)
        {
            string where = "name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", districtName }
                };
            List<District> districtList = await SelectDataAsync(where, parameters);
            return districtList.Count > 0 ? districtList[0] : null;
        }

        public async Task<List<District>> GetDistrictsByProvinceAsync(string provinceId)
        {
            string where = "province_id = @province_id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "province_id", provinceId }
                };
            return await SelectDataAsync(where, parameters);
        }

        protected override void Map(NpgsqlDataReader reader, District district)
        {
            district.Id = reader.GetString(reader.GetOrdinal("id"));
            district.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
            district.ProvinceId = reader.IsDBNull(reader.GetOrdinal("province_id")) ? null : reader.GetString(reader.GetOrdinal("province_id"));
        }
    }
}