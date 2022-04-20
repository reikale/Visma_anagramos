namespace AnagramSolver.Contracts.Models;

public class WordAnagramList
{
    public int Id { get; set; }
    public int AnagramId { get; set; }
    public string Word { get; set; }
    public string Category { get; set; }
}