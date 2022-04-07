namespace AnagramSolver.Contracts.Models;

public class Word
{
    public string Content { get; set; }
    public string Type { get; set; }

    public Word()
    {

    }

    public override int GetHashCode()
    {
        return string.Concat(Content.ToLower().OrderBy(c => c).ToArray()).GetHashCode();
    }
}