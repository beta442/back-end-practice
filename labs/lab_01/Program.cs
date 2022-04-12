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
                if (userInput != null &&
                    userInput.Contains(RENAME_BOARD_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (!userInput.Contains(' '))
                    {
                        Console.WriteLine("No name given. See help");
                    }
                    else
                    {
                        board.Rename(userInput.Split(' ').Skip(1).First());
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
                        board.AddColumn(userInput.Split(' ').Skip(1).First());
                    }
                }
                if (userInput != null &&
                    userInput.Contains(RENAME_COLUMN_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (userInput.Count(ch => ch == ' ') != 2)
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        board.RenameColumn(userInput.Split(' ').Skip(1).First(),
                            userInput.Split(' ').Skip(2).First());
                    }
                }
                if (userInput != null &&
                    userInput.Contains(ADD_TASK_COMMAND, StringComparison.OrdinalIgnoreCase))
                {
                    if (!(userInput.Count(ch => ch == ' ') >= 2 ||
                        userInput.Count(ch => ch == ' ') <= 4))
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        string taskName = userInput.Split(' ').Skip(1).First();
                        string taskDescription = userInput.Split(' ').Skip(2).First();
                        int taskPriority =
                            userInput.Count(ch => ch == ' ') >= 3
                            ? int.Parse(userInput.Split(' ').Skip(3).First())
                            : 0;
                        string columnName =
                            userInput.Count(ch => ch == ' ') == 4
                            ? userInput.Split(' ').Skip(4).First()
                            : "";
                        int columnIndex = board.GetColumnIndexByName(columnName);

                        if (userInput.Count(ch => ch == ' ') != 4)
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
                    if (userInput.Count(ch => ch == ' ') != 3)
                    {
                        Console.WriteLine("Wrong arguments count");
                    }
                    else
                    {
                        if (int.TryParse(userInput.Split(' ').Skip(2).First(), out int taskPriority))
                        {
                            string taskName = userInput.Split(' ').Skip(3).First();
                            string columnName = userInput.Split(' ').Skip(1).First();

                            int columnIndex = board.GetColumnIndexByName(columnName);
                            if (columnIndex == -1)
                            {
                                Console.WriteLine("No such column");
                            }
                            else
                            {
                                int taskIndex =
                                    board.GetPrioritedTaskIndexByNameFromColumn(columnIndex,
                                    taskPriority, taskName);
                                if (taskIndex != -1)
                                {
                                    board.RemoveTaskFromColumn(columnIndex, taskPriority, taskIndex);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("<taskPriority> should be an integer");
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
