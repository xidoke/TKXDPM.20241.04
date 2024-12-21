using AIMS.Models.Entities;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Services
{
    public class ProvinceService
    {
        private DatabaseConnect dbConnect = DatabaseConnect.gI();

        public async Task<List<Province>> GetAllProvincesAsync()
        {
            return await dbConnect.SelectDataAsync<Province>("Provinces", MapDataReaderToProvince);
        }

        public async Task<Province> GetProvinceByNameAsync(string provinceName)
        {
            string where = "name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "name", provinceName }
        };
            List<Province> provinceList = await dbConnect.SelectDataAsync<Province>("Provinces", MapDataReaderToProvince, where, parameters);
            return provinceList.Count > 0 ? provinceList[0] : null;
        }

        private Province MapDataReaderToProvince(NpgsqlDataReader reader)
        {
            return new Province
            {
                Id = reader.GetString(reader.GetOrdinal("id")),
                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"))
            };
        }
    }

}
