using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class AppTools
{
    public UITools _uiTools;
    private DictionaryActions _dictionaryActions;

    public AppTools(UITools uiTools, DictionaryActions dictionaryActions)
    {
        _uiTools = uiTools;
        _dictionaryActions = dictionaryActions;
    }
    public void StartProgram()
    {
        string userInput = _uiTools.AskQuestion("Enter the word to get its anagram:");
        List<Word> result = _dictionaryActions.CheckForAnagram(userInput);
        _uiTools.ShowTheResult(userInput, result);
    }

    public void LoadDictionary()
    {
        try
        {
            _dictionaryActions.DictionaryStartup();
        }
        catch (Exception exception)
        {
            _uiTools.FileNotFound(exception);
            throw;
        }
    }
}