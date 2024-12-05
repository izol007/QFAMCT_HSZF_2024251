using Microsoft.EntityFrameworkCore;
using QFAMCT_HSZF_2024251.Model;

namespace QFAMCT_HSZF_2024251.MsSql
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=RDZrtDB;Integrated Security=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => new { c.ClientID });

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Measurements)
                .WithOne()
                .HasForeignKey(c => c.ClientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Measurement>()
                .HasKey(m => new { m.Date, m.ClientID });
        }
    }
}
