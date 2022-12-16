using Microsoft.EntityFrameworkCore;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public class TCAPContext : DbContext
    {
        public TCAPContext(DbContextOptions<TCAPContext> options) : base(options)
        { }

		public DbSet<ChatLine> ChatLines { get; set; }
		public DbSet<ChatSession> ChatSessions { get; set; }
		public DbSet<Predator> Predators { get; set; }
		public DbSet<Decoy> Decoys { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// define the relationships between the entity classes
			modelBuilder.Entity<ChatSession>()
				.HasOne(s => s.Predator)
				.WithMany(p => p.ChatSessions)
				.HasForeignKey(s => s.PredatorId);
			modelBuilder.Entity<ChatSession>()
				.HasOne(s => s.Decoy)
				.WithMany(d => d.ChatSessions)
				.HasForeignKey(s => s.DecoyId);
			modelBuilder.Entity<ChatLine>()
				.HasOne(l => l.Chat)
				.WithMany(s => s.Lines)
				.HasForeignKey(l => l.ChatId);
		}
	}

}

