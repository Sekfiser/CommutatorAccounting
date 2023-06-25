using Microsoft.EntityFrameworkCore;

namespace CommutatorAccounting.Models
{
    public class CommutatorsContext : DbContext
    {
        public DbSet<Commutator> Commutators { get; set; } = null!;
        public CommutatorsContext(DbContextOptions<CommutatorsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
