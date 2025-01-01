using AIMS.Data.Entities.Address;

namespace AIMS.Repositories
{
    public interface IProvinceRepository
    {
        Task<List<Province>> GetAllAsync();
        Task<Province> GetById(string id);
    }
}
