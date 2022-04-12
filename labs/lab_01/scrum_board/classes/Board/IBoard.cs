namespace ScrumBoard.Board
{
    internal interface IBoard
    {
        void AddColumn(string name);
        void AddTaskIntoColumn(string taskName, string taskDescription, int taskPriority, int columnIndex = 0);
        List<string> GetAllColumnsNames();
        string GetBoardName();
        int GetColumnAmount();
        int GetColumnIndexByName(string columnName);
        string GetColumnNameByIndex(int columnIndex);
        int GetPrioritedTaskIndexByNameFromColumn(int columnIndex,
            int taskPriority, string taskName);
        void PrintBoard();
        void MoveColumnFromTo(int indexFrom, int indexTo);
        bool MoveTaskToAnotherColumn(int columnSourceIndex, int columnDestinationIndex, int taskPriority, int taskNumber);
        void Rename(string name);
        void RenameColumn(string prevName, string newName);
        public void RemoveTaskFromColumn(int columnIndex, int taskPriority, int taskNumber);
    }
}
