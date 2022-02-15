using Forum.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forum.Service
{
    public class ForumContext : DbContext
    {
        private readonly IConfiguration _config;

        public ForumContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            this._config = config;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(a => a.Answers)
                .WithOne(q => q.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasMany(c => c.Comments)
                .WithOne(a => a.Answer)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>(entitity =>
            {
                entitity.HasIndex(e => e.Email).IsUnique();
                entitity.HasIndex(e => e.UserName).IsUnique();
            });

        }
    }
}
