using AIMS.Data.Entities.Address;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IDistrictRepository
    {
        Task<List<District>> GetDistrictsByProvinceIdAsync(string provinceId);
    }
}
