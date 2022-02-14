using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface ICommentRepository
    {

        Task<bool> SaveChangesAsync();

        Task<Comment[]> GetUserCommentsByIdAsync(int userId);
        Task<Comment[]> GetCommentsByWord(string word);
        Task<bool> AlterVote(int id,bool up = true);


        Task<Comment> GetById(int id);

        Task<Comment[]> GetAllCommentsAsync();
    }
}
