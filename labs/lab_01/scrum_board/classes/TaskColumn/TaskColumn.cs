namespace ScrumBoard.TaskColumn
{
    internal class TaskColumn : ITaskColumn
    {
        private readonly Dictionary<int, List<ITask>> _prioritedTasks = new();
        private string _name;
        public TaskColumn(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public Dictionary<int, List<ITask>> GetPrioritedTaskMap()
        {
            return _prioritedTasks;
        }

        public ITask GetTaskBy(int taskPriority, int taskNumber)
        {
            if (taskNumber < 0 || !HasColumnPrioritedTasks(taskPriority) || taskNumber >= _prioritedTasks[taskPriority].Count)
            {
                throw new Exception("Failed find task");
            }

            return _prioritedTasks[taskPriority][taskNumber];
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
            if (taskNumber < 0 || !HasColumnPrioritedTasks(taskPriority) || taskNumber >= _prioritedTasks[taskPriority].Count)
            {
                throw new Exception("Failed to remove task from column");
            }

            _prioritedTasks[taskPriority].RemoveAt(taskNumber);
            if (_prioritedTasks[taskPriority].Count == 0)
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
    }
}
