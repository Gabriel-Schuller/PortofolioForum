using Forum.Data.Entities;
using Forum.Models;
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

        Task<bool> CheckForDuplicate(QuestionModel question);

        Task<Question> GetById(int id, bool includeAnswers = false);

        Task<Question[]> GetAllQuestionsAsync(bool includeAnswers = false, string orderBy= "Date");
    }
}
