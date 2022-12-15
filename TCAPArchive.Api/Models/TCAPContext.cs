using Microsoft.EntityFrameworkCore;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public class TCAPContext : DbContext
    {
        public TCAPContext(DbContextOptions<TCAPContext> options) : base(options)
        { }

        public DbSet<ChatLine> ChatLines { get; set; }
        public DbSet<Predator> Predators { get; set; }
        public DbSet<Decoy> Decoys { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }
}
