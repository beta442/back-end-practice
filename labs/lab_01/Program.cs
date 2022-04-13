using ScrumBoard.Board;

namespace ScrumBoardApplication
{
    class ScrumBoardProgram
    {
        private const string EXIT_COMMAND = "exit";
        private const string HELP_COMMAND = "help";
        private const string ADD_COLUMN_COMMAND = "addColumn";
        private const string ADD_TASK_COMMAND = "addTask";
        private const string REMOVE_TASK_COMMAND = "removeTask";
        private const string RENAME_COLUMN_COMMAND = "renameColumn";
        private const string RENAME_BOARD_COMMAND = "renameBoard";
        private const string PRINT_BOARD_COMMAND = "show";

        private static readonly Dictionary<string, string> s_commandsDescription = new() {
            { ADD_COLUMN_COMMAND, "Adding column with provided name, e.g. " + ADD_COLUMN_COMMAND + " <name>" },
            { ADD_TASK_COMMAND, "Adding new task into specified column, e.g. " + ADD_TASK_COMMAND + " <taskName> <taskDescription> [<taskPriority>, <columnName>], where <taskPriority> is integer" },
            { REMOVE_TASK_COMMAND, "Removing certain task from certain column, e.g. " + REMOVE_TASK_COMMAND + " <columnName> <taskPriority> <taskName>" },
            { RENAME_COLUMN_COMMAND, "Renames chosen column by given name, e.g. " + RENAME_COLUMN_COMMAND + " <previousName> <newName>"},
            { RENAME_BOARD_COMMAND, "Renames board, e.g. " + RENAME_BOARD_COMMAND + " <name>" },
            { PRINT_BOARD_COMMAND, "Show board's content"},
            { HELP_COMMAND, "Shows program usage" },
            { EXIT_COMMAND, "Exiting program" },
        };

        private static void PrintHelpMessage()
        {
            Console.WriteLine("Usage:");
            int longestCommandLength = s_commandsDescription.Keys.Max()!.Length;

            foreach (KeyValuePair<string, string> commandDiscription in s_commandsDescription)
            {
                Console.WriteLine("--" + commandDiscription.Key + ":" +
                    new string(' ', longestCommandLength - commandDiscription.Key.Length + 10) +
                    commandDiscription.Value);
            }
        }

        static int Main()
        {
            PrintHelpMessage();
            Console.WriteLine();

            IBoard board = new Board("ScrumbBoard");

            string? userInput = Console.ReadLine();
            while (!(userInput != null &&
                    userInput.Contains(EXIT_COMMAND, StringComparison.OrdinalIgnoreCase) &&
                    userInput.Length == EXIT_COMMAND.Length))
            {
                if (userInput != null &&
                    userInput.Contains(PRINT_BOARD_COMMAND, StringComparison.OrdinalIgnoreCase) &&
                    userInput.Length == PRINT_BOARD_COMMAND.Length)
                {
                    board.PrintBoard();
                    Console.WriteLine();
                }
                if (userInput != null &&
                    userInput.Contains(HELP_COMMAND, StringComparison.OrdinalIgnoreCase) &&
                    userInput.Length == HELP_COMMAND.Length)
                {
                    PrintHelpMessage();
                    Console.WriteLine();
                }

                string[] splitedArgs = userInput.Split(' ');
                if (userInput != null &&
                    userInput.Contains(RENAME_BOARD_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (!userInput.Contains(' '))
                    {
                        Console.WriteLine("No name given. See help");
                    }
                    else
                    {
                        board.Rename(splitedArgs.Skip(1).First());
                    }
                }
                if (userInput != null &&
                    userInput.Contains(ADD_COLUMN_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (!userInput.Contains(' '))
                    {
                        Console.WriteLine("No name given. See help");
                    }
                    else
                    {
                        board.AddColumn(splitedArgs.Skip(1).First());
                    }
                }
                if (userInput != null &&
                    userInput.Contains(RENAME_COLUMN_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (splitedArgs.Length != 3 || splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        board.RenameColumn(splitedArgs.Skip(1).First(),
                            splitedArgs.Skip(2).First());
                    }
                }
                if (userInput != null &&
                    userInput.Contains(ADD_TASK_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (!(splitedArgs.Length >= 3 && splitedArgs.Length <= 5) ||
                        splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        string taskName = splitedArgs.Skip(1).First();
                        string taskDescription = splitedArgs.Skip(2).First();
                        int taskPriority = 0;
                        if (splitedArgs.Length >= 3 && splitedArgs.Skip(3).First().Length != 0)
                        {
                            int.TryParse(splitedArgs.Skip(3).First(), out taskPriority);
                        }
                        string columnName =
                            splitedArgs.Length == 5
                            ? splitedArgs.Skip(4).First()
                            : "";
                        int columnIndex = board.GetColumnIndexByName(columnName);

                        if (splitedArgs.Length != 5)
                        {
                            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority);
                        }
                        else
                        {
                            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, columnIndex);
                        }
                    }
                }
                if (userInput != null &&
                    userInput.Contains(REMOVE_TASK_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (splitedArgs.Length != 4 || splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        try
                        {
                            int taskPriority = int.Parse(splitedArgs.Skip(2).First());

                            string taskName = splitedArgs.Skip(3).First();
                            string columnName = splitedArgs.Skip(1).First();

                            board.RemoveTaskFromColumn(columnName, taskPriority, taskName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                    userInput = Console.ReadLine();
            }

            Console.WriteLine("Shutting down");
            return 0;
        }
    }
}
