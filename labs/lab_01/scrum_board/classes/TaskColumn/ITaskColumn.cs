namespace ScrumBoard.TaskColumn
{
    internal interface ITaskColumn
    {
        void AddPrioritedTaskListInColumn(int taskPriority);
        void AddTask(ITask task);
        string GetName();
        Dictionary<int, List<ITask>> GetPrioritedTaskMap();
        ITask GetTaskBy(int taskPriority, int taskNumber);
        bool HasColumnPrioritedTasks(int taskPriority);
        void RemovePrioritedTaskListInColumn(int taskListPriority);
        void RemoveTask(int taskPriority, int taskNumber);
        void Rename(string name);
    }
}
