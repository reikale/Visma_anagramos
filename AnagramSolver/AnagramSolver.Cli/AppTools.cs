using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class AppTools
{
    public UITools _uiTools;
    private DictionaryController _dictionaryController;

    public AppTools(UITools uiTools, DictionaryController dictionaryController)
    {
        _uiTools = uiTools;
        _dictionaryController = dictionaryController;
    }
    public void StartProgram()
    {
        string userInput = _uiTools.AskQuestion("Enter the word to get its anagram:");
        //var start = DateTime.Now;
        List<Word> result = _dictionaryController.CheckForAnagram(userInput);
       
        _uiTools.ShowTheResult(userInput, result);
        //var end = DateTime.Now;
        //Console.WriteLine($"\nEnd of anagram search operation: {(end-start).TotalSeconds}");
    }

    public void LoadDictionary(string filePath)
    {
        try
        {
            _dictionaryController.DictionaryStartup(filePath);
        }
        catch (Exception exception)
        {
            _uiTools.FileNotFoundMesage(exception);
            throw;
        }
    }
}