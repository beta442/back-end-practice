using ScrumBoard.Board;
using ScrumBoard.TaskColumn;

namespace ScrumBoard.Factory
{
    using Board = Board.Board;
    using TaskColumn = TaskColumn.TaskColumn;
    using Task = Task.Task;

    public class ScrumBoardFactory
    {
        public static IBoard CreateBoard(string title)
        {
            return new Board(title);
        }

        public static ITaskColumn CreateColumn(string title)
        {
            return new TaskColumn(title);
        }

        public static ITask CreateTask(string title, string description, int priority)
        {
            return new Task(title, description, priority);
        }
    }
}
