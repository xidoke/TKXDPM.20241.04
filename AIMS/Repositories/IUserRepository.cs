using AIMS.Models.Entities;

namespace AIMS.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        string GetCurrentUserEmail();
    }
}
