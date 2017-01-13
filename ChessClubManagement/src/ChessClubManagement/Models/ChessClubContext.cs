using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChessClubManagement.Models
{
    public partial class ChessClubContext : DbContext
    {
        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<Seasons> Seasons { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Tally> Tally { get; set; }

        // Unable to generate entity type for table 'dbo.MatchesTrial'. Please see the warning messages.

        public ChessClubContext(DbContextOptions<ChessClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>(entity =>
            {
                entity.HasKey(e => e.MatchId)
                    .HasName("PK_Matches_MatchId");

                entity.Property(e => e.MatchDate).HasColumnType("datetime");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Matches__SeasonI__3D5E1FD2");

                entity.HasOne(d => d.Student1)
                    .WithMany(p => p.MatchesStudent1)
                    .HasForeignKey(d => d.Student1Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Matches__Student__37A5467C");

                entity.HasOne(d => d.Student2)
                    .WithMany(p => p.MatchesStudent2)
                    .HasForeignKey(d => d.Student2Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Matches__Student__38996AB5");
            });

            modelBuilder.Entity<Seasons>(entity =>
            {
                entity.HasKey(e => e.SeasonId)
                    .HasName("PK_Seasons");

                entity.Property(e => e.SeasonId).ValueGeneratedNever();

                entity.Property(e => e.SeasonName).HasMaxLength(50);

                entity.Property(e => e.Wk1).HasColumnType("date");

                entity.Property(e => e.Wk10).HasColumnType("date");

                entity.Property(e => e.Wk11).HasColumnType("date");

                entity.Property(e => e.Wk12).HasColumnType("date");

                entity.Property(e => e.Wk13).HasColumnType("date");

                entity.Property(e => e.Wk14).HasColumnType("date");

                entity.Property(e => e.Wk15).HasColumnType("date");

                entity.Property(e => e.Wk2).HasColumnType("date");

                entity.Property(e => e.Wk3).HasColumnType("date");

                entity.Property(e => e.Wk4).HasColumnType("date");

                entity.Property(e => e.Wk5).HasColumnType("date");

                entity.Property(e => e.Wk6).HasColumnType("date");

                entity.Property(e => e.Wk7).HasColumnType("date");

                entity.Property(e => e.Wk8).HasColumnType("date");

                entity.Property(e => e.Wk9).HasColumnType("date");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__Students__32C52B99ACDCD224");

                entity.Property(e => e.StudentDivision).HasColumnType("char(1)");

                entity.Property(e => e.StudentFname).HasMaxLength(50);

                entity.Property(e => e.StudentLname).HasMaxLength(50);
            });

            modelBuilder.Entity<Tally>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK_Tally");

                entity.Property(e => e.Number).ValueGeneratedNever();
            });
        }
    }
}