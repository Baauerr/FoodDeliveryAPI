using HITSBackEnd.baseClasses;
using HITSBackEnd.Services.Account;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.DataBase
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<BlackListToken> blackListTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListToken>()
                .HasKey(blackListToken => new { blackListToken.userEmail, blackListToken.Token });
        }
    }
}
