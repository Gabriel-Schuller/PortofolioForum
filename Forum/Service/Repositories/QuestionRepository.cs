using Forum.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Service.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ForumContext _context;

        public QuestionRepository(ForumContext context)
        {
            this._context = context;
        }
        public void Add<Question>(Question question)
        {
            _context.Add(question);
        }

        public void Delete<Question>(Question question)
        {
            _context.Remove(question);
        }

        public async Task<Question[]> GetAllQuestionsAsync(bool includeAnswers)
        {
            IQueryable<Question> query = _context.Questions;
            if (includeAnswers)
            {
                query= query.Include(q => q.Answers);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Question> GetById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<Question[]> GetQuestionsByWord(string word)
        {
            IQueryable<Question> query = _context.Questions.Where(q => q.Message.Contains(word));
            return await query.ToArrayAsync();
        }

        public async Task<Question[]> GetUserQuestionsByIdAsync(int userId)
        {
            IQueryable<Question> query = _context.Questions.Where(q => q.User.Id == userId);
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
