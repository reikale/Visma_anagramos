using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class UITools
{
    public string AskQuestion(string question)
    {
        string userInput = "";
        while (string.IsNullOrWhiteSpace(userInput) && userInput.Length < 3)
        {
            Console.WriteLine(question);
            userInput = Console.ReadLine(); // po šito veiksmo userInput string lieka be lietuviškų raidžių. Gal yra kaip tai pataisyt?
        }
        return userInput;
    }
    public void ShowTheResult(string userInput, List<Word> resultList)
    {
        Console.Clear();
        if (resultList.Count != 0)
        {
            Console.Write($"The anagram(s) for word '{userInput}': ");
            for (int i = 0; i < resultList.Count; i++)
            {
                Console.Write($"{resultList[i].Content}; ");
            }
        }
        else
        {
            AnagramWasNotFound(userInput);
        }
    }
    public void FileNotFoundMesage(Exception exception)
    {
        Console.WriteLine($"The file could not be found or read: {exception}");
    }

    public void AnagramWasNotFound(string userInput)
    {
        Console.WriteLine($"Sorry, but there is no anagram for the word '{userInput}'");
    }

    
   
}