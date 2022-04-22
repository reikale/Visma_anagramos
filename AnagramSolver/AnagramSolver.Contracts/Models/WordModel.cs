
namespace AnagramSolver.Contracts.Models;

public class WordModel 
{
    public int Id { get; set; }
    public string Word { get; set; }
    public string Category { get; set; }
    public override int GetHashCode()
    {
        return string.Concat(Word.ToLower().OrderBy(c => c).ToArray()).GetHashCode();
    }
}