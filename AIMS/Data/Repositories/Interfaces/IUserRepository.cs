using AIMS.Data.Entities;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUsername(string username);
        User GetByEmail(string email);
        void Add(User user);
        void Update(User user);
    }
}
