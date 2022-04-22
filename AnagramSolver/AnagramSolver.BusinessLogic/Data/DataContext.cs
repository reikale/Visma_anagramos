using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.Contracts.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
            
    }
    public DbSet<WordModel> Words { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<CachedWord> CachedWords { get; set; }
    
}