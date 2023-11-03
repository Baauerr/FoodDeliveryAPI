using HITSBackEnd.Services.Account;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Services.Orders;
using HITSBackEnd.Services.UserCart.UserCartRepository;
using HITSBackEnd.Services.UserRepository;
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
        
        public DbSet <OrdersDishesTable> OrdersDishes { get; set; }
        
        public DbSet <OrdersTable> Orders {  get; set; }
        public DbSet <RatingTable> DishesRating { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackListTokenTable>()
                .HasKey(blackListToken => new { blackListToken.userEmail, blackListToken.Token });
            modelBuilder.Entity<CartTable>()
                .HasKey(c => new { c.UserEmail, c.DishId });
            modelBuilder.Entity<OrdersTable>()
                .HasKey(c => new { c.UserEmail, c.Id });
            modelBuilder.Entity<OrdersDishesTable>()
                .HasKey(c => new { c.OrderId, c.DishId });
            modelBuilder.Entity<RatingTable>()
                .HasKey(c => new { c.UserEmail, c.DishId });
        }
    }
}
