using ScrumBoard.Board;

namespace ScrumBoardApplication
{
    class ScrumBoardProgram
    {
        private const string EXIT_COMMAND = "exit";
        private const string HELP_COMMAND = "help";
        private const string ADD_COLUMN_COMMAND = "addColumn";
        private const string ADD_TASK_COMMAND = "addTask";
        private const string MOVE_COLUMN_COMMAND = "moveColumn";
        private const string MOVE_TASK_FROM_COLUMN_TO_COLUMN = "moveTask";
        private const string REMOVE_TASK_COMMAND = "removeTask";
        private const string REMOVE_COLUMN_COMMAND = "removeColumn";
        private const string RENAME_COLUMN_COMMAND = "renameColumn";
        private const string PRINT_BOARD_COMMAND = "show";

        private static readonly Dictionary<string, string> s_commandsDescription = new()
        {
            {
                ADD_COLUMN_COMMAND,
                "Adding column with provided name, e.g. " +
                ADD_COLUMN_COMMAND + " <name>"
            },
            {
                ADD_TASK_COMMAND,
                "Adding new task into specified column, e.g. " +
                ADD_TASK_COMMAND + " <taskName> <taskDescription> [<taskPriority>, <columnName>]"
            },
            {
                MOVE_COLUMN_COMMAND,
                "Moving column, e.g. " + MOVE_COLUMN_COMMAND + " <indexFrom> <indexTo>"
            },
            {
                MOVE_TASK_FROM_COLUMN_TO_COLUMN,
                "Moving specified task from column to another column, e.g. " +
                MOVE_TASK_FROM_COLUMN_TO_COLUMN +
                " <fromColumnName> <toColumnName> <taskPriority> <taskNumber>"
            },
            {
                REMOVE_TASK_COMMAND,
                "Removing certain task from certain column, e.g. " +
                REMOVE_TASK_COMMAND + " <columnName> <taskPriority> <taskName>"
            },
            {
                REMOVE_COLUMN_COMMAND,
                "Removing specified column, e.g. " + REMOVE_COLUMN_COMMAND + " <columnName>"
            },
            {
                RENAME_COLUMN_COMMAND,
                "Renames chosen column by given name, e.g. " +
                RENAME_COLUMN_COMMAND + " <previousName> <newName>"
            },
            { PRINT_BOARD_COMMAND, "Show board's content" },
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

            bool isExitCommand = false;
            while (!isExitCommand)
            {
                Console.Write("> ");
                string? userInput = Console.ReadLine();
                if ((string.Compare(userInput, EXIT_COMMAND, true) == 0))
                {
                    isExitCommand = true;
                    continue;
                }

                if (string.Compare(userInput, PRINT_BOARD_COMMAND, true) == 0)
                {
                    board.PrintBoard();
                    Console.WriteLine();
                    continue;
                }
                if (string.Compare(userInput, HELP_COMMAND, true) == 0)
                {
                    PrintHelpMessage();
                    Console.WriteLine();
                    continue;
                }

                string[] splitedArgs = userInput.Split(' ');
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
                    continue;
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
                    continue;
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
                        if (splitedArgs.Length >= 4 && splitedArgs.Skip(3).First().Length != 0 &&
                            !int.TryParse(splitedArgs.Skip(3).First(), out taskPriority))
                        {
                            Console.WriteLine("Wrong <taskPriority> should be an integer");
                            continue;
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
                    continue;
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
                    continue;
                }
                if (userInput != null &&
                    userInput.Contains(REMOVE_COLUMN_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (splitedArgs.Length != 2 || splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        board.RemoveColumn(splitedArgs.Skip(1).First());
                    }
                    continue;
                }
                if (userInput != null &&
                    userInput.Contains(MOVE_COLUMN_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (splitedArgs.Length != 3 || splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        try
                        {
                            int sourceIndex = int.Parse(splitedArgs.Skip(1).First());
                            int destionationIndex = int.Parse(splitedArgs.Skip(2).First());
                            board.MoveColumnFromTo(sourceIndex, destionationIndex);
                        }
                        catch
                        {
                            Console.WriteLine("Each index should be an integer");
                        }
                    }
                    continue;
                }
                if (userInput != null &&
                    userInput.Contains(MOVE_TASK_FROM_COLUMN_TO_COLUMN, StringComparison.OrdinalIgnoreCase))
                {
                    if (splitedArgs.Length != 5 || splitedArgs.Any(arg => arg.Length == 0))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        try
                        {
                            string sourceColumnName = splitedArgs.Skip(1).First();
                            string destinationColumnName = splitedArgs.Skip(2).First();
                            if (!int.TryParse(splitedArgs.Skip(3).First(), out int taskPriority))
                            {
                                Console.WriteLine("<taskPriority> should be an integer");
                                continue;
                            }
                            string taskName = splitedArgs.Skip(4).First();
                            board.MoveTaskToAnotherColumn(sourceColumnName,
                                destinationColumnName, taskPriority, taskName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    continue;
                }
                Console.WriteLine("No such command. See help\n");
            }

            Console.WriteLine("Shutting down");
            return 0;
        }
    }
}
