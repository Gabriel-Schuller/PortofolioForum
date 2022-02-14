using System.Threading.Tasks;

namespace Forum.Service.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ForumContext _context;

        public BaseRepository(ForumContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
