using HITSBackEnd.Models.AddressModels;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.DataBase
{
    public class AddressesDbContext: DbContext
    {
        public AddressesDbContext(DbContextOptions<AddressesDbContext> options) : base(options) { }
        public DbSet<AsAddrObject> AsAddrObjects { get; set; }
        public DbSet<AsAdmHierarchy> AsAdmHierarchy { get; set; }
        public DbSet<AsHouses> AsHouses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=my_db;Username=postgres;Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //AsAddrObject

            modelBuilder.Entity<AsAddrObject>()
                .ToTable("as_addr_obj", schema: "public");

            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.id)
                .HasColumnName("id");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.objectId)
                .HasColumnName("objectid");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.objectGuid)
                .HasColumnName("objectguid");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.name)
                .HasColumnName("name");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.typename)
                .HasColumnName("typename");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.level)
                .HasColumnName("level");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.isActive)
                .HasColumnName("isactive");
            modelBuilder.Entity<AsAddrObject>()
                .Property(e => e.isActual)
                .HasColumnName("isactual");

            //AsAdmHierarchy

            modelBuilder.Entity<AsAdmHierarchy>()
                .ToTable("as_adm_hierarchy", schema: "public");

            modelBuilder.Entity<AsAdmHierarchy>()
                .Property(e => e.id)
                .HasColumnName("id");
            modelBuilder.Entity<AsAdmHierarchy>()
                .Property(e => e.objectId)
                .HasColumnName("objectid");
            modelBuilder.Entity<AsAdmHierarchy>()
                .Property(e => e.parentobjid)
                .HasColumnName("parentobjid");
            modelBuilder.Entity<AsAdmHierarchy>()
                .Property(e => e.isActive)
                .HasColumnName("isactive");

            //AsHouses
            modelBuilder.Entity<AsHouses>()
                .ToTable("as_houses", schema: "public");

            modelBuilder.Entity<AsHouses>()
                .Property(e => e.id)
                .HasColumnName("id");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.objectId)
                .HasColumnName("objectid");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.objectGuid)
                .HasColumnName("objectguid");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.houseType)
                .HasColumnName("housetype");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.houseNum)
                .HasColumnName("housenum");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.isActual)
                .HasColumnName("isactual");
            modelBuilder.Entity<AsHouses>()
                .Property(e => e.isActive)
                .HasColumnName("isactive");
        }
    }
}
