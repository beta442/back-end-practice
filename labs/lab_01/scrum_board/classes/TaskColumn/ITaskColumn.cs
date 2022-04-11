namespace ScrumBoard.TaskColumn
{
    internal interface ITaskColumn
    {
        string GetName();
        void Rename(string name);
        public Dictionary<int, List<ITask>> GetPrioritedTaskList();
    }
}
