using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IQuestionRepository
    {
        void Add<Question>(Question question);

        void Delete<Question>(Question question);

        Task<bool> SaveChangesAsync();

        Task<Question[]> GetUserQuestionsByIdAsync(int userId);
        Task<Question[]> GetQuestionsByWord(string word);

        Task<Question> GetById(int id);

        Task<Question[]> GetAllQuestionsAsync(bool includeAnswers = false);
    }
}
