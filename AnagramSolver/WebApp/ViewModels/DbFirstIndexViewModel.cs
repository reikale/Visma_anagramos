using AnagramSolver.Contracts.Models;

namespace WebApp.ViewModels;

public class DbFirstIndexViewModel
{
    public string UserWord { get; set; }
    public List<WordModel> Anagrams { get; set; }
}