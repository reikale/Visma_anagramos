using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts;

// VARIABLES
var appSettingsHandler = new AppSettingsHandler("appsettings.json");
var appSettings = appSettingsHandler.GetAppSettings();

// OBJECTS
DictionarySourceReader dictionarySourceReader = new DictionarySourceReader();
AnagramSolver.BusinessLogic.AnagramSolver anagramSolver = new AnagramSolver.BusinessLogic.AnagramSolver(dictionarySourceReader);
UITools uiTools = new UITools(appSettings);
AppTools appTools = new AppTools(uiTools, anagramSolver);


// APP STARTS HERE
appTools.StartProgram();