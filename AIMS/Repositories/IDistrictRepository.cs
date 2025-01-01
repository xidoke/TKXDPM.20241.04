using AIMS.Data.Entities.Address;

namespace AIMS.Repositories
{
    public interface IDistrictRepository
    {
        Task<List<District>> GetDistrictsByProvinceIdAsync(string provinceId);
        Task<District> GetById(string id);
    }
}
