namespace ScrumBoard.Factory
{
    public class ScrumBoardFactory
    {
        public static IBoard CreateBoard(string title)
        {
            return new Board(title);
        }

        public static ITaskColumn CreateColumn(string title)
        {
            return new TaskColumn.TaskColumn(title);
        }

        public static ITask CreateTask(string title, string description, int priority)
        {
            return new Task.Task(title, description, priority);
        }
    }
}
