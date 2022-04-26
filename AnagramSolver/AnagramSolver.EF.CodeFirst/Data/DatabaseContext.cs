using EF.CodeFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace EF.CodeFirst.Data;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<CachedWords> CachedWords { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<Words> Words { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}