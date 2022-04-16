namespace ScrumBoard.TaskColumn
{
    internal class TaskColumn : ITaskColumn
    {
        private readonly SortedDictionary<int, List<ITask>> _prioritedTasks = new();
        private string _name;

        public TaskColumn(string name)
        {
            if (name.Length == 0)
            {
                _name = "Column";
            }
            else
            {
                _name = name;
            }
        }

        public string GetName()
        {
            return _name;
        }

        public IReadOnlyDictionary<int, List<ITask>> GetPrioritedTaskMap()
        {
            return _prioritedTasks;
        }

        public ITask GetTaskBy(int taskPriority, int taskNumber)
        {
            if (taskNumber < 0 ||
                !HasColumnPrioritedTasks(taskPriority)
                || taskNumber >= GetPrioritedTaskCount(taskPriority))
            {
                throw new Exception("Failed find task");
            }

            return _prioritedTasks[taskPriority][taskNumber];
        }

        public int GetPrioritedTaskCount(int taskPriority)
        {
            if (!HasColumnPrioritedTasks(taskPriority))
            {
                return -1;
            }
            return _prioritedTasks[taskPriority].Count;
        }

        public int GetPrioritedTaskIndex(int taskPriority, string taskName)
        {
            if (taskName.Length == 0 || !_prioritedTasks.ContainsKey(taskPriority))
            {
                return -1;
            }
            return _prioritedTasks[taskPriority].FindIndex((task) => task.GetName() == taskName);
        }

        public int GetTaskCount()
        {
            int taskCount = 0;
            foreach (KeyValuePair<int, List<ITask>> prioritedTaskList in _prioritedTasks)
            {
                taskCount += prioritedTaskList.Value.Count;
            }
            return taskCount;
        }

        public void Rename(string name)
        {
            _name = name;
        }

        public bool HasColumnPrioritedTasks(int taskPriority)
        {
            return _prioritedTasks.ContainsKey(taskPriority);
        }

        public void AddTask(ITask task)
        {
            int newTaskPriority = task.GetPriority();
            if (!HasColumnPrioritedTasks(newTaskPriority))
            {
                _prioritedTasks.Add(newTaskPriority, new List<ITask>());
            }
            _prioritedTasks[newTaskPriority].Add(task);
        }

        public void RemoveTask(int taskPriority, int taskNumber)
        {
            if (taskNumber < 0 || !HasColumnPrioritedTasks(taskPriority) || taskNumber >= GetPrioritedTaskCount(taskPriority))
            {
                throw new Exception("Failed to remove task from column");
            }

            _prioritedTasks[taskPriority].RemoveAt(taskNumber);
            if (GetPrioritedTaskCount(taskPriority) == 0)
            {
                RemovePrioritedTaskListInColumn(taskPriority);
            }
        }

        public void AddPrioritedTaskListInColumn(int taskListPriority)
        {
            if (HasColumnPrioritedTasks(taskListPriority))
            {
                return;
            }
            _prioritedTasks.Add(taskListPriority, new List<ITask>());
        }

        public void RemovePrioritedTaskListInColumn(int taskListPriority)
        {
            if (!HasColumnPrioritedTasks(taskListPriority))
            {
                return;
            }
            _prioritedTasks.Remove(taskListPriority);
        }
        public void PrintContent()
        {
            Console.WriteLine(_name + " column.");
            foreach (KeyValuePair<int, List<ITask>> prioritedTasks in _prioritedTasks)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("  Priority #" + prioritedTasks.Key.ToString());
                foreach (ITask task in prioritedTasks.Value)
                {
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    task.PrintTask();
                }
            }
        }
    }
}
