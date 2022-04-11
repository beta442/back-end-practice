using ScrumBoard.TaskColumn;

namespace ScrumBoard.Board
{
    using TaskColumn = TaskColumn.TaskColumn;
    internal class Board
    {
        
        private readonly string _name;
        private List<ITaskColumn> _taskColumns = new();

        public Board(string name)
        {
            _name = name;
            _taskColumns.Add(new TaskColumn("Column name"));
        }

        public void AddColumn(string name)
        {
            if (name.Length == 0)
            {
                throw new Exception("Column can't have an empty name");
            }
            if (_taskColumns.Any(column => column.GetName() == name))
            {
                throw new Exception("The board already contains such a column");
            }
            if (_taskColumns.Count == 10)
            {
                throw new Exception("The board can contain no more than 10 columns");
            }

            _taskColumns.Add(new TaskColumn(name));
        }

        public int GetColumnIndexByName(string columnName)
        {
            return _taskColumns.FindIndex(column => column.GetName() == columnName);
        }

        public void MoveColumnFromTo(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexTo < 0 || indexFrom >= _taskColumns.Count)
            {
                return;
            }



        }

        public string GetBoardName()
        {
            return _name;
        }
    }
}
