using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;

// OBJECTS
DictionaryController dictionaryController = new DictionaryController();
WordRepo wordRepo = new WordRepo(dictionaryController);
UITools uiTools = new UITools();
AppTools appTools = new AppTools(uiTools, dictionaryController);
dictionaryController.ReceiveReferences(wordRepo);

// APP STARTS HERE
//var start = DateTime.Now;
appTools.LoadDictionary("../../../../zodynas.txt"); // trunka 44-52s
//var end = DateTime.Now;
//Console.WriteLine($"End of dictionary loading operation : {(end-start).TotalSeconds}"); 

appTools.StartProgram(); // trunka 0.04s