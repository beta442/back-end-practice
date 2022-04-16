namespace ScrumBoard
{
    public interface ITask
    {
        string GetDescription();
        string GetName();
        int GetPriority();
        void PrintTask();
    }
}
