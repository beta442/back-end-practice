namespace ScrumBoard.Board
{
    public interface IBoard
    {
        void AddColumn(string name);
        void AddTaskIntoColumn(string taskName,
            string taskDescription, int taskPriority, int columnIndex = 0);
        List<string> GetAllColumnsNames();
        string GetBoardName();
        int GetColumnAmount();
        int GetColumnIndexByName(string columnName);
        string GetColumnNameByIndex(int columnIndex);
        int GetPrioritedTaskIndexFromColumnBy(int columnIndex,
            int taskPriority, string taskName);
        void PrintBoard();
        void MoveColumnFromTo(int indexFrom, int indexTo);
        void MoveTaskToAnotherColumn(string columnSourceName,
            string columnDestinationName, int taskPriority, string taskName);
        void RenameColumn(string prevName, string newName);
        void RemoveColumn(string name);
        void RemoveTaskFromColumn(string columnName,
            int taskPriority, string taskName);
    }
}
