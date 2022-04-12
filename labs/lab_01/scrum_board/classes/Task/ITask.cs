namespace ScrumBoard
{
    internal interface ITask
    {
        string GetDescription();
        string GetName();
        int GetPriority();
        void PrintTask();
        void Rename(string name);
        void SetDescription(string description);
    }
}
