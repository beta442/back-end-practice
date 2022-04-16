using ScrumBoard.TaskColumn;
using System.Collections.ObjectModel;

namespace ScrumBoard.Board
{
    using Task = Task.Task;
    using TaskColumn = TaskColumn.TaskColumn;

    internal class Board : IBoard
    {
        private string _name;
        private readonly ObservableCollection<ITaskColumn> _taskColumns = new();

        public Board(string name)
        {
            _name = name;
        }

        public string GetBoardName()
        {
            return _name;
        }

        public int GetColumnIndexByName(string columnName)
        {
            for (int i = 0; i < _taskColumns.Count; ++i)
            {
                if (_taskColumns[i].GetName() == columnName)
                {
                    return i;
                }
            }
            return -1;
        }

        public string GetColumnNameByIndex(int columnIndex)
        {
            if (_taskColumns.Count <= columnIndex || columnIndex < 0)
            {
                throw new Exception("Index out of range while getting column name");
            }

            return _taskColumns[columnIndex].GetName();
        }

        public List<string> GetAllColumnsNames()
        {
            if (_taskColumns.Count == 0)
            {
                return new List<string>();
            }

            List<string> names = new();
            foreach (ITaskColumn column in _taskColumns)
            {
                names.Add(column.GetName());
            }

            return names;
        }

        public int GetColumnAmount()
        {
            return _taskColumns.Count;
        }

        public int GetPrioritedTaskIndexByNameFromColumn(int columnIndex,
            int taskPriority, string taskName)
        {
            if (taskName.Length == 0 ||
                columnIndex < 0 ||
                columnIndex >= _taskColumns.Count)
            {
                return -1;
            }

            return _taskColumns[columnIndex].GetPrioritedTaskIndex(taskPriority, taskName);
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

        public void MoveColumnFromTo(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexFrom >= _taskColumns.Count)
            {
                throw new Exception("Failed to move task, no such column to move");
            }
            if (indexTo < 0 || indexTo >= _taskColumns.Count)
            {
                throw new Exception("Failed to move task, can't move column to given index");
            }

            _taskColumns.Move(indexFrom, indexTo);
        }

        public void MoveTaskToAnotherColumn(string columnSourceName,
            string columnDestinationName, int taskPriority, string taskName)
        {

            if (!_taskColumns.Any(column => column.GetName() == columnSourceName))
            {
                throw new Exception("Failed to move task, no such source column");
            }
            if (!_taskColumns.Any(column => column.GetName() == columnDestinationName))
            {
                throw new Exception("Failed to move task, no such destination column");
            }

            int columnSourceIndex =
                _taskColumns.IndexOf(_taskColumns.First(column => column.GetName() == columnSourceName));
            if (!_taskColumns[columnSourceIndex].HasColumnPrioritedTasks(taskPriority))
            {
                throw new Exception("Failed to move task, no such priorited tasks");
            }
            int taskNumber =
                _taskColumns[columnSourceIndex].GetPrioritedTaskIndex(taskPriority, taskName);
            if (taskNumber == -1)
            {
                throw new Exception("Failed to move task, no such priorited task");
            }

            int columnDestinationIndex =
                _taskColumns.IndexOf(_taskColumns.First(column => column.GetName() == columnDestinationName));
            try
            {
                ITask task = _taskColumns[columnSourceIndex].GetTaskBy(taskPriority, taskNumber);
                _taskColumns[columnSourceIndex].RemoveTask(taskPriority, taskNumber);
                if (!_taskColumns[columnDestinationIndex].HasColumnPrioritedTasks(taskPriority))
                {
                    _taskColumns[columnDestinationIndex].AddPrioritedTaskListInColumn(taskPriority);
                }
                _taskColumns[columnDestinationIndex].AddTask(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddTaskIntoColumn(string taskName,
            string taskDescription, int taskPriority, int columnIndex = 0)
        {

            if (taskName.Length == 0)
            {
                throw new Exception("Failed to add task, task's name can't be empty");
            }
            if (taskDescription.Length == 0)
            {
                throw new Exception("Failed to add task, task's description can't be empty");
            }
            if (columnIndex < 0 || columnIndex >= _taskColumns.Count)
            {
                throw new Exception("Failed to add task, no such column, can't insert task");
            }
            if (_taskColumns.Count == 0)
            {
                throw new Exception("Failed to add task, board doesn't contain any column");
            }

            if (!_taskColumns[columnIndex].HasColumnPrioritedTasks(taskPriority))
            {
                _taskColumns[columnIndex].AddPrioritedTaskListInColumn(taskPriority);
            }

            Task task = new(taskName, taskDescription, taskPriority);

            _taskColumns[columnIndex].AddTask(task);
        }

        public void PrintBoard()
        {
            Console.WriteLine(_name + ':');
            foreach (ITaskColumn? taskColumn in _taskColumns)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
                taskColumn.PrintContent();
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
            }
        }

        public void RemoveTaskFromColumn(string columnName, int taskPriority, string taskName)
        {
            if (columnName.Length == 0 || !_taskColumns.Any(column => column.GetName() == columnName))
            {
                throw new Exception("Failed to remove task, no such column");
            }

            int columnIndex =
                _taskColumns.IndexOf(_taskColumns.First(column => column.GetName() == columnName));
            if (!_taskColumns[columnIndex].HasColumnPrioritedTasks(taskPriority))
            {
                throw new Exception("Failed to remove task, no such priorited tasks in column");
            }
            if (taskName.Length == 0 ||
                _taskColumns[columnIndex].GetPrioritedTaskIndex(taskPriority, taskName) == -1)
            {
                throw new Exception("Failed to remove task, no such task in column");
            }

            try
            {
                int taskNumber = _taskColumns[columnIndex].GetPrioritedTaskIndex(taskPriority, taskName);
                _taskColumns[columnIndex].RemoveTask(taskPriority, taskNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveColumn(string name)
        {
            if (!_taskColumns.Any(column => column.GetName() == name))
            {
                throw new Exception("Failed remove column, no such column");
            }

            _taskColumns.Remove(_taskColumns.First(column => column.GetName() == name));
        }

        public void RenameColumn(string prevName, string newName)
        {
            if (prevName.Length == 0 || newName.Length == 0)
            {
                throw new Exception("Wrong param length");
            }
            try
            {
                GetColumnByName(prevName).Rename(newName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private ITaskColumn GetColumnByName(string name)
        {
            foreach (ITaskColumn? column in _taskColumns)
            {
                if (column.GetName() == name)
                {
                    return column;
                }
            }
            throw new Exception("No such column");
        }
    }
}
