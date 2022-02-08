using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IUserRepository
    {
        void Add<User>(User user);

        void Delete<User>(User user);

        Task<bool> SaveChangesAsync();


        Task<User> GetById(int id, bool includeQuestions = false);

        Task<User[]> GetAllUsersAsync(bool includeQuestions = false, bool IncludeAnswers = false);

        Task<User[]> GetUsersByWord(string word);

    }
}
