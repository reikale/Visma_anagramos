using System.Net.Http.Json;
using System.Text.Json;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Cli;

public class AppTools
{
    private readonly UITools _uiTools;

    public AppTools(UITools uiTools)
    {
        _uiTools = uiTools;
    }
    public void StartProgram()
    {
        string userInput = _uiTools.AskQuestion("Enter the word to get its anagram:");
        List<Words> result = new List<Words>();
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
                    var readTask = clientResult.Content.ReadFromJsonAsync<List<Words>>();
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