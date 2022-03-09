
namespace Discount.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.Configuration;
    public partial class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }
        public DiscountContext() : base(GetOptions(ConfigurationManager.ConnectionStrings["ConnectionStrings:DBContext"].ConnectionString.ToString()))
        {
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder().UseLazyLoadingProxies(), connectionString).Options;
        }

        public virtual DbSet<UserData> UserData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
