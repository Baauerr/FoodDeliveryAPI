using HITSBackEnd.Services.Account;
using HITSBackEnd.Services.Account.UserRepository;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Services.UserCart.UserCartRepository;
using Microsoft.EntityFrameworkCore;


namespace HITSBackEnd.DataBase
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsersTable> Users { get; set; }
        public DbSet<BlackListTokenTable> BlackListTokens { get; set; }

        public DbSet <DishTable> Dishes { get; set; }

        public DbSet <CartTable> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListTokenTable>()
                .HasKey(blackListToken => new { blackListToken.userEmail, blackListToken.Token });
            modelBuilder.Entity<CartTable>()
                .HasKey(c => new { c.UserEmail, c.DishId });
        }
    }
}
