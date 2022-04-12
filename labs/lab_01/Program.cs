using ScrumBoard.Board;

void PrintBoardColumnsNames(Board board)
{
    List<string> columnNames = board.GetAllColumnsNames();

    foreach (string name in columnNames)
    {
        Console.WriteLine("--" + name);
    }
}

Board board = new("Project workflow");
Console.WriteLine(board.GetBoardName());

Console.WriteLine("Column names at board's init state:");
PrintBoardColumnsNames(board);

string columnName;
for (int i = 2; i < 14; i++)
{
    columnName = "Column #" + i.ToString();
    try
    {
        board.AddColumn(columnName);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Tried create column with name '" + columnName + "'");
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine("Column names after board filled up with columns:");
PrintBoardColumnsNames(board);

board.MoveColumnFromTo(3, 9);
Console.WriteLine("Column names after moving 4 column to 10:");
PrintBoardColumnsNames(board);

board.AddTaskIntoColumn("Task0", "Task0 description", 0);
board.AddTaskIntoColumn("Task1", "Task1 description", 0);
board.AddTaskIntoColumn("Task2", "Task3 description", 0);
board.AddTaskIntoColumn("Task3", "Task5 description", 0);

Console.WriteLine("First column's content:");
board.PrintColumnContent(0);

Console.WriteLine("Second column's content before task moving:");
board.PrintColumnContent(1);

if (!board.MoveTaskToAnotherColumn(0, 1, 0, 2))
{
    Console.WriteLine("Failed to move task from first to second column");
}
Console.WriteLine("Second column's content after task moving:");
board.PrintColumnContent(1);
