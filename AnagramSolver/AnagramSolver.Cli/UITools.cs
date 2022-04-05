using AnagramSolver.BusinessLogic;

namespace AnagramSolver.Cli;

public class UITools
{
    private DictionaryController _dictionaryController;
    public UITools(DictionaryController dictionaryController)
    {
        _dictionaryController = dictionaryController;
    }
    
    public string AskQuestion(string question)
    {
        bool isOn = true;
        string userInput = "";
        while (isOn)
        {
            Console.WriteLine(question);
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.Clear();
                Console.WriteLine("User input is invalid. Try again");
            }
            isOn = false;
        }
        return userInput;
    }

    public void ShowTheResult(string userInput, string resultString)
    {
        Console.Clear();
        Console.WriteLine($"The anagram(s) for word '{userInput}': {resultString}");
    }

    public void StartProgram()
    {
        string userInput = AskQuestion("Enter the word to get its anagram:");
        string result = _dictionaryController.CheckForAnagram(userInput);
        ShowTheResult(userInput, result);
    }
}