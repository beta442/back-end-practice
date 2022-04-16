using Xunit;

using ScrumBoard;
using ScrumBoard.Factory;

namespace ScrumBoardTest
{
    public class TaskColumnUnitTest
    {
        [Fact]
        public void Created_column_named_correctly()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);

            Assert.True(taskColumn.GetName() == taskColumnName);
        }

        [Fact]
        public void Created_column_doesnt_contain_any_task()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);

            Assert.True(taskColumn.GetTaskCount() == 0);
            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 0);
        }

        [Fact]
        public void Created_column_has_default_name()
        {
            const string emptyColumnName = "";
            const string expectedColumnName = "Column";
            ITaskColumn taskColumn = MockColumn(emptyColumnName);

            Assert.True(taskColumn.GetName() == expectedColumnName);
        }

        [Fact]
        public void Adding_priorited_task_list_in_column_adds_it()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;

            taskColumn.AddPrioritedTaskListInColumn(taskPriority);

            Assert.True(taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 1);
        }

        [Fact]
        public void Adding_priorited_task_list_twicly_gives_no_effect()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;

            taskColumn.AddPrioritedTaskListInColumn(taskPriority);
            taskColumn.AddPrioritedTaskListInColumn(taskPriority);

            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 1);
        }

        [Fact]
        public void Adding_task_into_column_adds_task()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);

            taskColumn.AddTask(task);

            Assert.True(taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskMap().ContainsKey(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskCount(taskPriority) == 1);
            Assert.True(taskColumn.GetTaskCount() == 1);
        }

        private ITaskColumn MockColumn(string title)
        {
            return ScrumBoardFactory.CreateColumn(title);
        }

        private ITask MockTask(string taskName, string taskDescription, int taskPriority)
        {
            return ScrumBoardFactory.CreateTask(taskName, taskDescription, taskPriority);
        }
    }
}
