using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DatabaseFirst.Model
{
    public partial class AnagramSolverContext : DbContext
    {
        public AnagramSolverContext()
        {
        }

        public AnagramSolverContext(DbContextOptions<AnagramSolverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; } = null!;
        public virtual DbSet<UserLog> UserLogs { get; set; } = null!;
        public virtual DbSet<WordModel> Words { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=AnagramSolver;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.Property(e => e.SearchedWord).HasMaxLength(255);

                entity.HasOne(d => d.Anagrams)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CachedWords_Words");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.FoundAnagrams).HasMaxLength(255);

                entity.Property(e => e.SearchString).HasMaxLength(255);

                entity.Property(e => e.SearchTime).HasColumnType("datetime");

                entity.Property(e => e.UserIp)
                    .HasMaxLength(255)
                    .HasColumnName("UserIP");
            });

            modelBuilder.Entity<WordModel>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.Word1)
                    .HasMaxLength(255)
                    .HasColumnName("Word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
