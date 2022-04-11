using ScrumBoard.TaskColumn;
using System.Collections.ObjectModel;

namespace ScrumBoard.Board
{
    using Task = Task.Task;
    using TaskColumn = TaskColumn.TaskColumn;
    internal class Board
    {
        
        private readonly string _name;
        private readonly ObservableCollection<ITaskColumn> _taskColumns = new();

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

        public void MoveColumnFromTo(int indexFrom, int indexTo)
        {
            if (indexFrom < 0 || indexTo < 0 || indexFrom >= _taskColumns.Count || indexTo >= _taskColumns.Count)
            {
                return;
            }

            _taskColumns.Move(indexFrom, indexTo);
        }

        public void PrintColumnContent(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _taskColumns.Count || _taskColumns[columnIndex].GetPrioritedTaskList().Count == 0)
            {
                return;
            }

            uint taskPriority = 0;
            foreach (KeyValuePair<int, List<ITask>> prioritedTaskList in _taskColumns[columnIndex].GetPrioritedTaskList())
            {
                Console.WriteLine("Tasks with " + taskPriority.ToString() + " priority:");
                foreach (ITask task in prioritedTaskList.Value)
                {
                    Console.WriteLine("--Name: " + task.GetName());
                    Console.WriteLine("--Description: " + task.GetDescription());
                }
                taskPriority++;
            }
        }

        public void AddTaskIntoColumn(string taskName, string taskDescription, int taskPriority, int columnIndex = 0)
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
                throw new Exception("Given column index is out of ragne, can't insert task");
            }
            if (!_taskColumns[columnIndex].GetPrioritedTaskList().ContainsKey(taskPriority))
            {
                _taskColumns[columnIndex].GetPrioritedTaskList().Add(taskPriority, new List<ITask>());
            }

            Task task = new(taskName, taskDescription, taskPriority);

            _taskColumns[columnIndex].GetPrioritedTaskList()[taskPriority].Add(task);
        }

        public string GetBoardName()
        {
            return _name;
        }

        public int GetColumnAmount()
        {
            return _taskColumns.Count;
        }
    }
}
