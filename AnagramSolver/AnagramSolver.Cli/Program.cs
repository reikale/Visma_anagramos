using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;

// OBJECTS
DictionaryController dictionaryController = new DictionaryController();
UITools uiTools = new UITools(dictionaryController);

// APP STARTS HERE
dictionaryController.DictionaryStartup();
uiTools.StartProgram();