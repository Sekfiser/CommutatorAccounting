using Microsoft.EntityFrameworkCore;

namespace CommutatorAccounting.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Commutator> Commutators { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
