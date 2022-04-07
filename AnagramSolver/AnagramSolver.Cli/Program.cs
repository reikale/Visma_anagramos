using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts;

// VARIABLES
var appSettingsHandler = new AppSettingsHandler("appsettings.json");
var appSettings = appSettingsHandler.GetAppSettings();

// OBJECTS
DictionarySourceReader dictionarySourceReader = new DictionarySourceReader(appSettings);
DictionaryActions dictionaryActions = new DictionaryActions(dictionarySourceReader);
UITools uiTools = new UITools(appSettings);
AppTools appTools = new AppTools(uiTools, dictionaryActions);

// APP STARTS HERE
appTools.LoadDictionary();
appTools.StartProgram();