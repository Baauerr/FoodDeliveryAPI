using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.DataBase
{
    public class AddressesDbContext: DbContext
    {
        public AddressesDbContext(DbContextOptions<AddressesDbContext> options) : base(options) { }
    }
}
