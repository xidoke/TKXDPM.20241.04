using AIMS.Data.Entities.Address;

namespace AIMS.Repositories
{
    public interface IWardRepository
    {
        Task<List<Ward>> GetWardsByDistrictIdAsync(string districtId);
        Task<Ward> GetById(string id);
    }
}
