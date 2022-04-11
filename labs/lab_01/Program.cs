using ScrumBoard.Board;

void PrintBoardColumns(Board board)
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
PrintBoardColumns(board);

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
PrintBoardColumns(board);

board.MoveColumnFromTo(3, 9);
Console.WriteLine("Column names after moving 4 column to 10:");
PrintBoardColumns(board);

board.AddTaskIntoColumn("Task0", "Task0 description", 0);
board.AddTaskIntoColumn("Task1", "Task1 description", 0);
board.AddTaskIntoColumn("Task3", "Task3 description", 0);
board.AddTaskIntoColumn("Task5", "Task5 description", 0);

Console.WriteLine("First column's content: ");
board.PrintColumnContent(0);
