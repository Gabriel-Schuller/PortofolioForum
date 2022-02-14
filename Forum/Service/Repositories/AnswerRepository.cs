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

        public async Task<bool> AlterVote(int id, bool up = true)
        {
            var answer = await GetById(id);
            if (up)
            {
                answer.Votes++;
            }
            else
            {
                answer.Votes--;
            }
            return await this.SaveChangesAsync();
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

        public async Task<Answer> GetById(int id, bool includeComments = false)
        {
            IQueryable<Answer> query = _context.Answers;
            if (includeComments)
            {
                query = query.Include(q => q.Comments);
            }
            query = query.Where(a => a.Id == id);
            return await query.FirstOrDefaultAsync();
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
