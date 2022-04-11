namespace ScrumBoard
{
    internal interface ITask
    {
        string GetDescription();
        string GetName();
        int GetPriority();
        void Rename(string name);
        void SetDescription(string description);
        void SetPriority(int priority);
    }
}
