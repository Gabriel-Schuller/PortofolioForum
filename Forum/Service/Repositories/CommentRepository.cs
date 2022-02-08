using Forum.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Service.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumContext _context;

        public CommentRepository(ForumContext context)
        {
            this._context = context;
        }

        public void Add<Comment>(Comment comment)
        {
            _context.Add(comment);
        }

        public async Task<bool> AlterVote(int id, bool up = true)
        {
            var comment = await GetById(id);
            if (up)
            {
                comment.Votes++;
            }
            else
            {
                comment.Votes--;
            }
            return await this.SaveChangesAsync();
        }

        public void Delete<Comment>(Comment comment)
        {
            _context.Remove(comment);
        }

        public async Task<Comment[]> GetAllCommentsAsync()
        {
            return await _context.Comments.ToArrayAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment[]> GetCommentsByWord(string word)
        {
            IQueryable<Comment> query = _context.Comments.Where(c => c.Message.Contains(word));
            return await query.ToArrayAsync();
        }

        public async Task<Comment[]> GetUserCommentsByIdAsync(int userId)
        {
            IQueryable<Comment> query = _context.Comments.Where(c => c.User.Id == userId);
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
