namespace ScrumBoard.Task
{
    internal class Task : ITask
    {
        private string _name;
        private string _description;
        private int _priority;
        public Task(string name, string description, int priority)
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

        public int GetPriority()
        {
            return _priority;
        }

        public void SetPriority(int priority)
        {
            _priority = priority;
        }
    }
}
