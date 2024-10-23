using Microsoft.EntityFrameworkCore;
using TestWork.Models.Entity;

namespace TestWork.DataAccess
{
    public class TestWorkDbContext(IConfiguration configuration) : DbContext
    {
        private const string DBCONTEXTKEY = "PostgreSQL";
        private const string DELIVERYORDERTIMETYPE = "timestamp without time zone";
        private const string AREANAMETYPE = "varchar(255)";

        private readonly IConfiguration _configuration = configuration;

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Area> Areas => Set<Area>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(DBCONTEXTKEY));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(p => p.DeliveryOrderTime)
                .HasColumnType(DELIVERYORDERTIMETYPE);

            modelBuilder.Entity<Area>()
                .Property(p => p.AreaName)
                .HasColumnType(AREANAMETYPE)
                .IsRequired(true);
        }
    }
}