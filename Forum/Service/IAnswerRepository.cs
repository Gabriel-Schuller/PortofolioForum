using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IAnswerRepository
    {
        void Add<Answer>(Answer answer);

        void Delete<Answer>(Answer answer);

        Task<bool> SaveChangesAsync();

        Task<Answer[]> GetUserAnswersByIdAsync(int userId);
        Task<Answer[]> GetAnswersByWord(string word);

        Task<Answer> GetById(int id);

        Task<Answer[]> GetAllAnswersAsync(bool includeComments = false);
    }
}
