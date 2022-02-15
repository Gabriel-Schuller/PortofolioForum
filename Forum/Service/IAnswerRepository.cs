using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface IAnswerRepository
    {

       

        Task<Answer[]> GetUserAnswersByIdAsync(int userId);
        Task<Answer[]> GetAnswersByWord(string word);
        Task<bool> AlterVote(int id, bool up = true);


        Task<Answer> GetById(int id, bool includeComments = false);

        Task<Answer[]> GetAllAnswersAsync(bool includeComments = false);
    }
}
