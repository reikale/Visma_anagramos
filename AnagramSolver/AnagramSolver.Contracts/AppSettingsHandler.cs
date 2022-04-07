using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
namespace AnagramSolver.Contracts;

public class AppSettingsHandler
{
    private string _filename;
    private AppSettings _config;

    public AppSettingsHandler(string filename)
    {
        _filename = filename;
        _config = GetAppSettings();
    }

    public AppSettings GetAppSettings()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(_filename, false, true)
            .Build();

        return config.GetSection("App").Get<AppSettings>();
    }
}