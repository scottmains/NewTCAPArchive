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

         modelBuilder.Entity<ChatSession>()
        .HasOne(cs => cs.Predator)
        .WithMany(p => p.ChatSessions)
        .OnDelete(DeleteBehavior.Cascade)
        .HasForeignKey(cs => cs.PredatorId);

        modelBuilder.Entity<ChatSession>()
        .HasOne(cs => cs.Decoy)
        .WithMany(d => d.ChatSessions)
        .OnDelete(DeleteBehavior.Cascade)
        .HasForeignKey(cs => cs.DecoyId);

         modelBuilder.Entity<ChatLine>()
         .HasOne(cl => cl.ChatSession)
         .WithMany(cs => cs.ChatLines)
         .OnDelete(DeleteBehavior.Cascade)
         .HasForeignKey(cl => cl.ChatSessionId);

        }

    }
}

