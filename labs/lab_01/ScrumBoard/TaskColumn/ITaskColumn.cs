namespace ScrumBoard.TaskColumn
{
    internal interface ITaskColumn
    {
        string GetName();
        void Rename(string name);
        List<List<ITask>> GetPrioritedTaskList();
    }
}
