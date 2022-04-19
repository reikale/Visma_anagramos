using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.Contracts.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
            
    }
    public DbSet<WordModel> Words { get; set; }
}