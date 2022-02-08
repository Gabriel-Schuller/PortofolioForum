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
        Task<bool> AlterVote(int id, bool up = true);


        Task<Answer> GetById(int id, bool includeComments = false);

        Task<Answer[]> GetAllAnswersAsync(bool includeComments = false);
    }
}
