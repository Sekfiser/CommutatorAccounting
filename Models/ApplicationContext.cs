using Microsoft.EntityFrameworkCore;

namespace CommutatorAccounting.Models
{
    public class CommutatorsContext : DbContext
    {
        public DbSet<Commutator> Commutators { get; set; } = null!;
        public CommutatorsContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public CommutatorsContext(DbContextOptions<CommutatorsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
