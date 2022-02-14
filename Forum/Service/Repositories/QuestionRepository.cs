using Forum.Data.Entities;
using Forum.Models;
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
        

        public async Task<bool> AlterVote(int id, bool up = true)
        {
            var question=await GetById(id);
            if (up)
            {
                question.Votes++;
            }
            else
            {
                question.Votes--;
            }
            return await this.SaveChangesAsync();
            
        }

        public async Task<bool> CheckForDuplicate(QuestionModel question)
        {
            var questions = await GetQuestionsByWord(question.Message);
            if (questions.Length == 0)
            {
                
                questions = await GetQuestionsByWord(question.Title);
                if (questions.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<Question[]> GetAllQuestionsAsync(bool includeAnswers, string orderBy = "Date")
        {
            IQueryable<Question> query = _context.Questions;
            if (includeAnswers)
            {
                query = query.Include(q => q.Answers);
            }


            switch (orderBy)
            {
                case "Title":
                    query = query.OrderBy(q => q.Title);
                    break;
                case "Votes":
                    query = query.OrderBy(q => q.Votes);
                    break;
                case "Date":
                    query = query.OrderBy(q => q.Date);
                    break;
                case "AnswerNumber":
                    query = query.OrderBy(q => q.Answers.Count());
                    break;
                default:
                    break;
            }

            return await query.ToArrayAsync();
        }

        public async Task<Question> GetById(int id, bool includeAnswers = false)
        {
            IQueryable<Question> query= _context.Questions;
            if (includeAnswers)
            {
                query = query.Include(q => q.Answers);
            }
            query = query.Where(q => q.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Question[]> GetQuestionsByWord(string word)
        {
            IQueryable<Question> query = _context.Questions.Where(q => q.Message.Contains(word) || q.Title.Contains(word));
            Question[] questions = await query.ToArrayAsync();
            
            return questions;
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
