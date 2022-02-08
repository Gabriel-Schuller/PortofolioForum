using Forum.Data.Entities;
using System.Threading.Tasks;

namespace Forum.Service
{
    public interface ICommentRepository
    {
        void Add<Comment>(Comment comment);

        void Delete<Comment>(Comment comment);

        Task<bool> SaveChangesAsync();

        Task<Comment[]> GetUserCommentsByIdAsync(int userId);
        Task<Comment[]> GetCommentsByWord(string word);

        Task<Comment> GetById(int id);

        Task<Comment[]> GetAllCommentsAsync();
    }
}
