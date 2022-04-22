using System.Net.Http.Json;
using System.Text.Json;
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
        List<WordModel> result = new List<WordModel>();
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7009/api/anagram/");
                //HTTP GET
                var clientRresponse = client.GetAsync(userInput);
                clientRresponse.Wait();
                var clientResult = clientRresponse.Result;
                if (clientResult.IsSuccessStatusCode)
                {
                    var readTask = clientResult.Content.ReadFromJsonAsync<List<WordModel>>();
                    readTask.Wait();
                    result = readTask.Result;
                }
            }
            //Older version:
            //result = _anagramSolver.CheckForAnagram(userInput);
        }
        catch (Exception e)
        {
            _uiTools.FileNotFound(e);
            throw;
        }
        _uiTools.ShowTheResult(userInput, result);
    }
}