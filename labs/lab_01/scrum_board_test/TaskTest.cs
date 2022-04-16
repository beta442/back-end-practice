using Xunit;

using ScrumBoard;
using ScrumBoard.Factory;

namespace ScrumBoardTest
{
    public class TaskUnitTest
    {
        [Fact]
        public void C()
        {

        }

        private ITask MockTask(string taskName, string taskDescription, int taskPriority)
        {
            return ScrumBoardFactory.CreateTask(taskName, taskDescription, taskPriority);
        }
    }
}
