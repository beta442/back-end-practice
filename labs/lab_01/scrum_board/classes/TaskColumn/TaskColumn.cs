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

        public Dictionary<int, List<ITask>> GetPrioritedTaskList()
        {
            return _prioritedTasks;
        }

        public void Rename(string name)
        {
            _name = name;
        }

        public void AddTask(ITask task)
        {
            int newTaskPriority = task.GetPriority();
            if (!_prioritedTasks.ContainsKey(newTaskPriority))
            {
                _prioritedTasks.Add(newTaskPriority, new List<ITask>());
            }
            _prioritedTasks[newTaskPriority].Add(task);
        }

        public bool RemoveTask(int taskPriority, int taskNumber)
        {
            if (!_prioritedTasks.ContainsKey(taskPriority) || _prioritedTasks[taskPriority].Count <= taskNumber)
            {
                return false;
            }

            _prioritedTasks[taskPriority].RemoveAt(taskNumber);
            return true;
        }
    }
}
