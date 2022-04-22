using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// VARIABLES
var appSettingsHandler = new AppSettingsHandler("appsettings.json");
var appSettings = appSettingsHandler.GetAppSettings();

// OBJECTS
var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var connectionString = config.GetConnectionString("WebAppContext");
optionsBuilder.UseSqlServer(connectionString);

DataContext dbContext = new DataContext(optionsBuilder.Options);

// To use txt file as source:
TextFileRepository textFileRepository = new TextFileRepository();
// To use database as source:
DatabaseRepository databaseRepository = new DatabaseRepository(dbContext);

AnagramSolver.BusinessLogic.AnagramSolver anagramSolver = new AnagramSolver.BusinessLogic.AnagramSolver(databaseRepository);
UITools uiTools = new UITools(appSettings);
AppTools appTools = new AppTools(uiTools, anagramSolver);


// APP STARTS HERE
appTools.StartProgram();