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

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
