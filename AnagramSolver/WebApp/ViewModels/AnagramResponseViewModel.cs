using AnagramSolver.Contracts.Models;

namespace WebApp.ViewModels;

public class AnagramResponseViewModel
{
    public string UserWord { get; set; }
    public List<Word> Anagrams { get; set; }
}