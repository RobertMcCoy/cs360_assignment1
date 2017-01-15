using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChessClubManagement.Models
{
    public partial class ChessClubContext : DbContext
    {
        public virtual DbSet<Divisions> Divisions { get; set; }
        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<Seasons> Seasons { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Tally> Tally { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'dbo.MatchesTrial'. Please see the warning messages.

        public ChessClubContext(DbContextOptions<ChessClubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Divisions>(entity =>
            {
                entity.HasKey(e => e.DivisionId)
                    .HasName("PK__Division__20EFC6A8BD846D79");

                entity.Property(e => e.DivisionName).HasMaxLength(55);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SeasonId_Divisions_Seasons");
            });

            modelBuilder.Entity<Matches>(entity =>
            {
                entity.HasKey(e => e.MatchId)
                    .HasName("PK_Matches_MatchId");

                entity.Property(e => e.MatchDate).HasColumnType("datetime");

                entity.Property(e => e.Student1Color).HasColumnType("char(1)");

                entity.Property(e => e.Student1Result).HasColumnType("char(1)");

                entity.Property(e => e.Student1Score).HasColumnType("decimal");

                entity.Property(e => e.Student2Color).HasColumnType("char(1)");

                entity.Property(e => e.Student2Result).HasColumnType("char(1)");

                entity.Property(e => e.Student2Score).HasColumnType("decimal");

                entity.Property(e => e.TotalMoves).HasDefaultValueSql("0");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SeasonId_Matches_Seasons");

                entity.HasOne(d => d.Student1)
                    .WithMany(p => p.MatchesStudent1)
                    .HasForeignKey(d => d.Student1Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Student1Id_Matches_Students");

                entity.HasOne(d => d.Student2)
                    .WithMany(p => p.MatchesStudent2)
                    .HasForeignKey(d => d.Student2Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Student2Id_Matches_Students");
            });

            modelBuilder.Entity<Seasons>(entity =>
            {
                entity.HasKey(e => e.SeasonId)
                    .HasName("PK__Seasons__C1814E38FA29E485");

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

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK_DivisionId_Students_Divisions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserId_Students_Users");
            });

            modelBuilder.Entity<Tally>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK_Tally");

                entity.Property(e => e.Number).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Nickname).HasColumnType("varchar(255)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(25)");
            });
        }
    }
}