using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class UITools
{
    private AppSettings _appSettings;

    public UITools(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public string AskQuestion(string question)
    {
        string userInput = "";
        while (string.IsNullOrWhiteSpace(userInput) && userInput.Length < _appSettings.Client.MinInputLength)
        {
            Console.WriteLine(question);
            userInput = Console.ReadLine();
        }
        return userInput;
    }
    public void ShowTheResult(string userInput, List<Words>? resultList)
    {
        Console.Clear();
        if (resultList.Count != 0)
        {
            AnagramResults(userInput, resultList);
        }
        else
        {
            AnagramNotFound(userInput);
        }
    }
    public void FileNotFound(Exception exception)
    {
        Console.WriteLine($"The file could not be found or read: {exception}");
    }

    private void AnagramNotFound(string userInput)
    {
        Console.WriteLine($"Sorry, but there is no anagram for the word '{userInput}'");
    }

    private void AnagramResults(string userInput, List<Words>? resultList)
    {
        Console.Write($"The anagram(s) for word '{userInput}': ");
        int numberOfAnagrams = resultList.Count;
        if (resultList.Count > _appSettings.Anagrams.MaxNumberOfReturningAnagrams)
        {
            numberOfAnagrams = _appSettings.Anagrams.MaxNumberOfReturningAnagrams;
        }
        for (int i = 0; i < numberOfAnagrams; i++)
        {
            Console.Write($"{resultList[i].Word}; ");
        }
    }
}