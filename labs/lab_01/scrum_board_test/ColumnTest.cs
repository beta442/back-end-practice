using Xunit;

using ScrumBoard;
using ScrumBoard.Factory;
using System;

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
            const int expectedTaskNumber = 0;
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);

            taskColumn.AddTask(task);

            Assert.True(taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskMap().ContainsKey(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskCount(taskPriority) == 1);
            Assert.True(taskColumn.GetTaskCount() == 1);
            Assert.True(taskColumn.GetTaskBy(taskPriority, expectedTaskNumber) == task);
        }

        [Fact]
        public void Attempt_to_find_non_existing_task_fails()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            const int expectedTaskNumber = 0;
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);
            taskColumn.AddTask(task);

            Assert.Throws<Exception>(() => taskColumn.GetTaskBy(1, expectedTaskNumber));
            Assert.Throws<Exception>(() => taskColumn.GetTaskBy(taskPriority, -900));
            Assert.Throws<Exception>(() => taskColumn.GetTaskBy(taskPriority, 15000));
        }

        [Fact]
        public void Created_column_doesnt_contain_priorited_task()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);

            Assert.True(taskColumn.GetPrioritedTaskCount(0) == -1);
            Assert.True(taskColumn.GetPrioritedTaskIndex(0, "SomeName") == -1);
        }

        [Fact]
        public void Renaming_column_renames_it()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const string expectedName = "newName";

            taskColumn.Rename(expectedName);

            Assert.True(taskColumn.GetName() == expectedName);
        }

        [Fact]
        public void Removing_task_removes_it()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            const int expectedTaskNumber = 0;
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);
            taskColumn.AddTask(task);

            taskColumn.RemoveTask(taskPriority, expectedTaskNumber);

            Assert.True(taskColumn.GetTaskCount() == 0);
            Assert.True(!taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskCount(taskPriority) == -1);
            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 0);
        }

        [Fact]
        public void Attempt_to_remove_task_with_wrong_parameters_fails()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const int unexpectedTaskPriority = 15;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            const int expectedTaskNumber = 0;
            const int unexpectedTaskNumber = 10;
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);
            taskColumn.AddTask(task);

            Assert.Throws<Exception>(() => taskColumn.RemoveTask(unexpectedTaskPriority, expectedTaskNumber));
            Assert.Throws<Exception>(() => taskColumn.RemoveTask(taskPriority, unexpectedTaskNumber));
        }

        [Fact]
        public void Removing_priorited_task_list_in_column_removes_it()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);
            taskColumn.AddTask(task);

            taskColumn.RemovePrioritedTaskListInColumn(taskPriority);

            Assert.True(!taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskCount(taskPriority) == -1);
            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 0);
        }

        [Fact]
        public void Removing_priorited_task_list_with_wrong_parameters_gives_no_effect()
        {
            const string taskColumnName = "TestColumn";
            ITaskColumn taskColumn = MockColumn(taskColumnName);
            const int taskPriority = 10;
            const string taskName = "Task1";
            const string taskDescriprtion = "Task1's description";
            ITask task = MockTask(taskName, taskDescriprtion, taskPriority);
            taskColumn.AddTask(task);

            taskColumn.RemovePrioritedTaskListInColumn(-500);

            Assert.True(taskColumn.HasColumnPrioritedTasks(taskPriority));
            Assert.True(taskColumn.GetPrioritedTaskCount(taskPriority) == 1);
            Assert.True(taskColumn.GetPrioritedTaskMap().Count == 1);
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
