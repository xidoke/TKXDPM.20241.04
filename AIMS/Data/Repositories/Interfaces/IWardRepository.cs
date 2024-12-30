using AIMS.Data.Entities.Address;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IWardRepository
    {
        Task<List<Ward>> GetWardsByDistrictIdAsync(string districtId);
    }
}
