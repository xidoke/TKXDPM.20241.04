using AIMS.Models.Entities;
using AIMS.Services.impl;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Services
{
    public class ProvinceService : BaseService<ProvinceService, Province>, IProvinceService
    {
        public async Task<List<Province>> GetAllProvincesAsync()
        {
            return await SelectDataAsync();
        }

        public async Task<Province> GetProvinceByNameAsync(string provinceName)
        {
            string where = "name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", provinceName }
                };
            List<Province> provinceList = await SelectDataAsync(where, parameters);
            return provinceList.Count > 0 ? provinceList[0] : null;
        }

        protected override void Map(NpgsqlDataReader reader, Province province)
        {
            province.Id = reader.GetString(reader.GetOrdinal("id"));
            province.Name = reader.IsDBNull(reader.GetOrdinal("name"))
                ? null : reader.GetString(reader.GetOrdinal("name"));
        }
    }

}
