using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class UITools
{
    private AppSettings _appSettings;
    private readonly int _minInputLength;
    private readonly int _maxNumberOfAnagrams;

    public UITools(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _minInputLength = _appSettings.Client.MinInputLength;
        _maxNumberOfAnagrams = _appSettings.Anagrams.MaxNumberOfReturningAnagrams;
    }
    public string AskQuestion(string question)
    {
        string userInput = "";
        while (string.IsNullOrWhiteSpace(userInput) && userInput.Length < _minInputLength)
        {
            Console.WriteLine(question);
            userInput = Console.ReadLine();
        }
        return userInput;
    }
    public void ShowTheResult(string userInput, List<Word> resultList)
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

    public void AnagramNotFound(string userInput)
    {
        Console.WriteLine($"Sorry, but there is no anagram for the word '{userInput}'");
    }

    public void AnagramResults(string userInput, List<Word> resultList)
    {
        Console.Write($"The anagram(s) for word '{userInput}': ");
        int numberOfAnagrams = resultList.Count;
        if (resultList.Count > _maxNumberOfAnagrams)
        {
            numberOfAnagrams = _maxNumberOfAnagrams;
        }
        for (int i = 0; i < numberOfAnagrams; i++)
        {
            Console.Write($"{resultList[i].Content}; ");
        }
    }
}