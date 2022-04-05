using System.Text;

namespace AnagramSolver.BusinessLogic;

public class DictionaryController
{
    private readonly string _FILE_PATH = "../../../../zodynas.txt";
    private Dictionary<string, List<string>> _DICTIONARY = new Dictionary<string, List<string>>();
    public void DictionaryStartup()
    {
        // all logic from loading to transforming data to dictionary(data stucture)
        var allRawWords = ReturnItemFromSource(0).Distinct();
        _DICTIONARY = ReturnWordDictionary(allRawWords);
    }
    public List<string> ReturnItemFromSource(int position)
    {
        try
        {
            List<string> listOfSourceObjects = new List<string>();
            using (StreamReader sr = new StreamReader(_FILE_PATH))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var sourceObject = line.Split("\t")[position];
                    listOfSourceObjects.Add(sourceObject);
                }
            }

            return listOfSourceObjects;
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }
    public Dictionary<string, List<string>> ReturnWordDictionary(IEnumerable<string> separatedWordList)
    {
        Dictionary<string, List<string>> wordDictionary = new Dictionary<string, List<string>>();
        foreach (string word in separatedWordList)
        {
            string wordCode = CalculateWordCode(word);
            if (wordDictionary.ContainsKey(wordCode))
            {
                wordDictionary[wordCode].Add(word);
            }
            else
            {
                wordDictionary.Add(wordCode, new List<string>{word});
            }
        }

        
        return wordDictionary;
    }

    public string CheckForAnagram(string userInput)
    {
        var testCode = CalculateWordCode(userInput);

        var test = _DICTIONARY[testCode];
        var builder = new StringBuilder();
        foreach (var zodis in test)
        {
            builder.Append(zodis + " ");
        }
        return builder.ToString();
    }

    public string CalculateWordCode(string word)
    {
        var summary = new Dictionary<char, int>();
        var newWord = String.Concat(word.ToLower().OrderBy(c => c));
        
        foreach (char letter in newWord)
        {
            if (summary.ContainsKey(letter))
            {
                summary[letter]++;
            }
            else
            {
                summary.Add(letter, 1);
            }
        }
        var returningCode = new StringBuilder();

        foreach (var pair in summary)
        {
            var builder = new StringBuilder();
            builder.Append(pair.Value).Append(pair.Key);
            returningCode.Append(builder.ToString());
        }
        return returningCode.ToString();
    }
}