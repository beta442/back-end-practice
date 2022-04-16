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

        public string GetDescription()
        {
            return _description;
        }

        public int GetPriority()
        {
            return _priority;
        }

        public void PrintTask()
        {
            Console.WriteLine("Title: " + _name);
            Console.WriteLine("Description: " + _description);
        }
    }
}
