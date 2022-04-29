using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Data;
using EF.CodeFirst.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// VARIABLES
var appSettingsHandler = new AppSettingsHandler("appsettings.json");
var appSettings = appSettingsHandler.GetAppSettings();

// OBJECTS
var optionsBuilder1 = new DbContextOptionsBuilder<DataContext>();
var optionsBuilder2 = new DbContextOptionsBuilder<DatabaseContext>();

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var connectionString1 = config.GetConnectionString("WebAppContext");
optionsBuilder1.UseSqlServer(connectionString1);
DataContext dbContext = new DataContext(optionsBuilder1.Options);

var connectionString2 = config.GetConnectionString("DatabaseContext");
optionsBuilder2.UseSqlServer(connectionString2);
DatabaseContext databaseContext = new DatabaseContext(optionsBuilder2.Options);

// To use txt file as source:
TextFileRepository textFileRepository = new TextFileRepository();
// To use database as source:
AnagramRepository databaseRepository = new AnagramRepository(dbContext, databaseContext);
CacheRepository cacheRepository = new CacheRepository(dbContext, databaseContext);

AnagramSolver.BusinessLogic.AnagramSolver anagramSolver = new AnagramSolver.BusinessLogic.AnagramSolver(databaseRepository, cacheRepository);
UITools uiTools = new UITools(appSettings);
AppTools appTools = new AppTools(uiTools);


// APP STARTS HERE
appTools.StartProgram();