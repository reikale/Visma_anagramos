namespace AnagramSolver.Contracts.Models;

public class CachedWord
{
    public int Id { get; set; }
    public string SearchedWord { get; set; }
    public int AnagramsId { get; set; }
}