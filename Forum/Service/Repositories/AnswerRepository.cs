using Forum.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Service.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ForumContext _context;

        public AnswerRepository(ForumContext context)
        {
            this._context = context;
        }
        public void Add<Answer>(Answer answer)
        {
            _context.Add(answer);
        }

        public void Delete<Answer>(Answer answer)
        {
            _context.Remove(answer);
        }

        public async Task<Answer[]> GetAllAnswersAsync(bool includeComments = false)
        {
            IQueryable<Answer> query = _context.Answers;
            if (includeComments)
            {
                query = query.Include(a => a.Comments);
            }

            return await query.ToArrayAsync();
        }


        public  async Task<Answer[]> GetAnswersByWord(string word)
        {
            IQueryable<Answer> query = _context.Answers.Where(a => a.Message.Contains(word));
            return await query.ToArrayAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<Answer[]> GetUserAnswersByIdAsync(int userId)
        {
            IQueryable<Answer> query = _context.Answers.Where(a => a.User.Id == userId);
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
