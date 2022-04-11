namespace ScrumBoard.Task
{
    internal class Task : ITask
    {
        private string _name;
        private string _description;
        private uint _priority;
        public Task(string name, string description, uint priority)
        {
            _name = name;
            _description = description;
            _priority = priority;
        }

        public string GetName()
        {
            return _name;
        }

        public void Rename(string name)
        {
            _name = name;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public ulong GetPriority()
        {
            return _priority;
        }

        public void SetPriority(uint priority)
        {
            _priority = priority;
        }
    }
}
