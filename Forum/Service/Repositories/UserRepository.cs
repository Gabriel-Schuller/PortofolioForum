using Forum.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumContext _context;

        public UserRepository(ForumContext context)
        {
            this._context = context;
        }

        public void Add<User>(User user)
        {
            _context.Add(user);
        }

        public void Delete<User>(User user)
        {
            _context.Remove(user);
        }


        public async Task<User[]> GetAllUsersAsync(bool includeQuestions, bool IncludeAnswers)
        {
            IQueryable<User> query = _context.Users;

            if (includeQuestions)
            {
                query = query.Include(u => u.Questions);
            }
            if (IncludeAnswers)
            {
                query = query.Include(u => u.Answers);
            }

            return await query.ToArrayAsync();
        }

        public async Task<User> GetById(int id, bool includeQuestions = false)
        {
            IQueryable<User> query = _context.Users;
            if (includeQuestions)
            {
                query = query.Include(q => q.Questions);
            }
            query = query.Where(u => u.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<User[]> GetUsersByWord(string word)
        {
            IQueryable<User> query = _context.Users.Where(u => u.UserName.Contains(word));
            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
