using EF.CodeFirst.Model;

namespace WebApp.ViewModels;

public class CodeFirstIndexViewModel
{
    public string UserWord { get; set; }
    public List<Words> Anagrams { get; set; }
}