namespace ScrumBoard.TaskColumn
{
    internal class TaskColumn : ITaskColumn
    {
        private readonly List<List<ITask>> _prioritedTasks = new();
        private string _name;
        public TaskColumn(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public void Rename(string name)
        {
            _name = name;
        }

        public void AddTask(ITask task)
        {
            ulong newTaskPriority = task.GetPriority();
            _prioritedTasks.ElementAt((int)newTaskPriority).Add(task);
        }

        public bool RemoveTask(int taskPriority, int taskNumber)
        {
            if (_prioritedTasks.Count < taskPriority || _prioritedTasks[taskPriority].Count < taskNumber)
            {
                return false;
            }

            _prioritedTasks[taskPriority].RemoveAt(taskNumber);
            return true;
        }
    }
}
