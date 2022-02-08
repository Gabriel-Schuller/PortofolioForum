using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IUserRepository
    {
        void Add<User>(User user);

        void Delete<User>(User user);

        Task<bool> SaveChangesAsync();


        Task<User> GetById(int id);

        Task<User[]> GetAllUsersAsync(bool includeQuestions = false, bool IncludeAnswers = false);

    }
}
