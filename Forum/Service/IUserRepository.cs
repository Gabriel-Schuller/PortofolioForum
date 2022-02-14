using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IUserRepository
    {
        

        Task<bool> SaveChangesAsync();


        Task<User> GetById(int id, bool includeQuestions = false);

        Task<User[]> GetAllUsersAsync(bool includeQuestions = false, bool IncludeAnswers = false);

        Task<User[]> GetUsersByWord(string word);

    }
}
