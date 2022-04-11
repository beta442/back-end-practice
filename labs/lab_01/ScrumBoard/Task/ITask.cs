namespace ScrumBoard
{
    internal interface ITask
    {
        string GetDescription();
        string GetName();
        ulong GetPriority();
        void Rename(string name);
        void SetDescription(string description);
        void SetPriority(uint priority);
    }
}
