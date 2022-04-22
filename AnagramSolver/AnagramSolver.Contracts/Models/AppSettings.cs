namespace AnagramSolver.Contracts.Models;

public class AppSettings
{
    public WordsRepoSourceConfiguration WordsRepoSource { get; set; }
    public AnagramsConfiguration Anagrams { get; set; }
    public ClientConfiguration Client { get; set; }
    public string ConnectionString { get; set; }
}