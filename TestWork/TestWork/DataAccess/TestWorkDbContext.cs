using Microsoft.EntityFrameworkCore;
using TestWork.Models.Entity;

namespace TestWork.DataAccess
{
    public class TestWorkDbContext(IConfiguration configuration) : DbContext
    {
        private const string DBCONTEXTKEY = "PostgreSQL";

        private readonly IConfiguration _configuration = configuration;

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<СityDistrict> СityDistricts => Set<СityDistrict>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(DBCONTEXTKEY));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<СityDistrict>()
                .Property(p => p.СityDistrictName)
                .HasColumnType("smallserial");

            modelBuilder.Entity<Order>()
                .Property(p => p.DeliveryOrderTime)
                .HasColumnType("timestamp");

            modelBuilder.Entity<Order>()
                .Property(p => p.OrderId)
                .HasColumnType("serial");
        }
    }
}