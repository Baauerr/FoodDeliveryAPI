using HITSBackEnd.Services.Account;
using HITSBackEnd.Services.Account.UserRepository;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Services.UserCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HITSBackEnd.DataBase
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<BlackListToken> BlackListTokens { get; set; }

        public DbSet <Dish> Dishes { get; set; }

        public DbSet <Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListToken>()
                .HasKey(blackListToken => new { blackListToken.userEmail, blackListToken.Token });
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.UserEmail, c.DishId });
        }
       
    }
}
