using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class AppTools
{
    private readonly UITools _uiTools;
    private readonly BusinessLogic.AnagramSolver _anagramSolver;

    public AppTools(UITools uiTools, BusinessLogic.AnagramSolver anagramSolver)
    {
        _uiTools = uiTools;
        _anagramSolver = anagramSolver;
    }
    public void StartProgram()
    {
        string userInput = _uiTools.AskQuestion("Enter the word to get its anagram:");
        List<Word> result;
        try
        {
            result = _anagramSolver.CheckForAnagram(userInput);
        }
        catch (Exception e)
        {
            _uiTools.FileNotFound(e);
            throw;
        }
        _uiTools.ShowTheResult(userInput, result);
    }
}