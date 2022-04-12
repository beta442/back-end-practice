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
            try
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            _taskColumns.Add(new TaskColumn(name));
        }

        public void MoveColumnFromTo(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexTo < 0 || indexFrom >= _taskColumns.Count ||
                indexTo >= _taskColumns.Count)
            {
                return;
            }

            _taskColumns.Move(indexFrom, indexTo);
        }

        public bool MoveTaskToAnotherColumn(int columnSourceIndex,
            int columnDestinationIndex, int taskPriority, int taskNumber)
        {
            if (columnSourceIndex < 0 || columnSourceIndex >= _taskColumns.Count ||
                columnDestinationIndex < 0 || columnDestinationIndex >= _taskColumns.Count ||
                !_taskColumns[columnSourceIndex].HasColumnPrioritedTasks(taskPriority) ||
                taskNumber < 0)
            {
                return false;
            }

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

            return true;
        }
        public void AddTaskIntoColumn(string taskName,
            string taskDescription, int taskPriority, int columnIndex = 0)
        {
            try
            {
                if (taskName.Length == 0)
                {
                    throw new Exception("Task's name can't be empty");
                }
                if (taskDescription.Length == 0)
                {
                    throw new Exception("Task's description can't be empty");
                }
                if (columnIndex < 0 || columnIndex >= _taskColumns.Count)
                {
                    throw new Exception("Given column index is out of range, can't insert task");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
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
                taskColumn.PrintContent();
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
            }
        }

        public void Rename(string name)
        {
            if (name.Length == 0)
            {
                return;
            }
            _name = name;
        }

        public void RemoveTaskFromColumn(int columnIndex, int taskPriority, int taskNumber)
        {
            if (columnIndex < 0 ||
                columnIndex >= _taskColumns.Count ||
                _taskColumns[columnIndex].GetPrioritedTaskCount(taskPriority) == -1 ||
                taskNumber < 0 ||
                taskNumber >= _taskColumns[columnIndex].GetPrioritedTaskCount(taskPriority))
            {
                return;
            }

            _taskColumns[columnIndex].RemoveTask(taskPriority, taskNumber);
        }

        public void RenameColumn(string prevName, string newName)
        {
            try
            {
                if (prevName.Length == 0 || newName.Length == 0)
                {
                    throw new Exception("Wrong param length");
                }
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
