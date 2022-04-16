using Xunit;

using ScrumBoard;
using ScrumBoard.Factory;

namespace ScrumBoardTest
{
    public class TaskUnitTest
    {
        [Fact]
        public void Created_task_has_correct_properties()
        {
            const string taskName = "TaskName";
            const string taskDescription = "tASkDescRiptiOn";
            const int taskPriority = 1000;

            ITask task = MockTask(taskName, taskDescription, taskPriority);

            Assert.True(task.GetName() == taskName);
            Assert.True(task.GetDescription() == taskDescription);
            Assert.True(task.GetPriority() == taskPriority);
        }

        private ITask MockTask(string taskName, string taskDescription, int taskPriority)
        {
            return ScrumBoardFactory.CreateTask(taskName, taskDescription, taskPriority);
        }
    }
}
