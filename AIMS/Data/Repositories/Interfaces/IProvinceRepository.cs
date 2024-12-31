using AIMS.Data.Entities.Address;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IProvinceRepository
    {
        Task<List<Province>> GetAllAsync();
        Task<Province> GetById(string id);
    }
}
