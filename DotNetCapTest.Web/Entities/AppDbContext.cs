using Microsoft.EntityFrameworkCore;

namespace DotNetCapTest.Web.Entities
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
