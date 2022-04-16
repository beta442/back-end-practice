using Xunit;

using ScrumBoard;
using ScrumBoard.Factory;
using System;

namespace ScrumBoardTest
{
    public class BoardUnitTest
    {
        [Fact]
        public void Created_board_named_correctly()
        {
            const string boardName = "TestBoardName";
            IBoard board = MockBoard(boardName);

            Assert.True(board.GetBoardName() == boardName);
        }

        [Fact]
        public void Board_has_default_name_if_given_name_was_empty()
        {
            IBoard board = MockBoard("");

            Assert.True(board.GetBoardName() == "ScrumBoard");
        }

        [Fact]
        public void Adding_column_into_board_creates_column_in_board()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "TestColumn";

            board.AddColumn(columnName);

            Assert.True(board.GetColumnIndexByName(columnName) != -1);
        }

        [Fact]
        public void Adding_two_columns_with_equal_names_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "TestColumn";

            board.AddColumn(columnName);

            Assert.Throws<Exception>(() => board.AddColumn(columnName));
            Assert.True(board.GetAllColumnsNames().Count == 1);
            Assert.True(board.GetAllColumnsNames()[0] == columnName);
        }

        [Fact]
        public void Adding_column_with_empty_name_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "";

            Assert.Throws<Exception>(() => board.AddColumn(columnName));
            Assert.True(board.GetAllColumnsNames().Count == 0);
        }

        [Fact]
        public void Adding_column_when_board_has_more_than_10_columns_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";

            for (int i = 0; i < 10; ++i)
            {
                board.AddColumn(columnName + i.ToString());
            }

            Assert.Throws<Exception>(() => board.AddColumn(columnName + "10"));
            Assert.True(board.GetAllColumnsNames().Count == 10);
            Assert.True(!board.GetAllColumnsNames().Contains(columnName + "10"));
        }

        [Fact]
        public void Adding_task_with_empty_name_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string emptyTaskName = "";
            board.AddColumn(columnName);

            Assert.Throws<Exception>(() => board.AddTaskIntoColumn(emptyTaskName, "desc", 0, 0));
        }

        [Fact]
        public void Adding_task_with_empty_description_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string taskName = "task1";
            const string emptyTaskDescription = "";
            board.AddColumn(columnName);

            Assert.Throws<Exception>(() => board.AddTaskIntoColumn(taskName, emptyTaskDescription, 0, 0));
        }

        [Fact]
        public void Adding_task_with_wrong_given_column_index_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string taskName = "task1";
            const string taskDescription = "task1's description";
            const int wrongColumnIndexFirst = -1;
            const int wrongColumnIndexSecond = 8;
            board.AddColumn(columnName);

            Assert.Throws<Exception>(
                () => board.AddTaskIntoColumn(taskName, taskDescription, 0, wrongColumnIndexFirst));
            Assert.Throws<Exception>(
                () => board.AddTaskIntoColumn(taskName, taskDescription, 0, wrongColumnIndexSecond));
        }

        [Fact]
        public void Adding_priorited_task_into_column_creates_priorited_task()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string taskName = "task1";
            const string taskDescription = "task1's description";
            const int taskPriority = 1;
            board.AddColumn(columnName);
            int columnIndex = board.GetColumnIndexByName(columnName);

            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, columnIndex);

            Assert.True(
                board.GetPrioritedTaskIndexFromColumnBy(columnIndex, taskPriority, taskName) == 0);
        }

        [Fact]
        public void Created_board_doesnt_contain_any_column()
        {
            IBoard board = MockBoard("TestBoard");

            Assert.True(board.GetColumnAmount() == 0);
            Assert.True(board.GetAllColumnsNames().Count == 0);
            Assert.Throws<Exception>(() => board.GetColumnNameByIndex(0));
        }

        [Fact]
        public void New_columns_in_board_are_indexed_correctly()
        {
            IBoard board = MockBoard("TestBoard");
            const int firstColumnExpectedIndex = 0;
            const int secondColumnExpectedIndex = 1;
            const int thirdColumnExpectedIndex = 2;
            const int tenthColumnExpectedIndex = 9;
            const int unexpectedColumnIndex = -1;
            const string columnNameTemplate = "columnName";

            for (int i = 0; i < 10; ++i)
            {
                board.AddColumn(columnNameTemplate + i.ToString());
            }

            Assert.True(
                board.GetColumnIndexByName(columnNameTemplate + "0") == firstColumnExpectedIndex);
            Assert.True(
                board.GetColumnIndexByName(columnNameTemplate + "1") == secondColumnExpectedIndex);
            Assert.True(
                board.GetColumnIndexByName(columnNameTemplate + "2") == thirdColumnExpectedIndex);
            Assert.True(
                board.GetColumnIndexByName(columnNameTemplate + "9") == tenthColumnExpectedIndex);
            Assert.True(
                board.GetColumnIndexByName("abracadabra") == unexpectedColumnIndex);

            Assert.True(
                board.GetColumnNameByIndex(firstColumnExpectedIndex) == columnNameTemplate + "0");
            Assert.True(
                board.GetColumnNameByIndex(secondColumnExpectedIndex) == columnNameTemplate + "1");
            Assert.True(
                board.GetColumnNameByIndex(thirdColumnExpectedIndex) == columnNameTemplate + "2");
            Assert.True(
                board.GetColumnNameByIndex(tenthColumnExpectedIndex) == columnNameTemplate + "9");
        }

        [Fact]
        public void Created_board_doesnt_contain_any_priorited_task()
        {
            IBoard board = MockBoard("TestBoard");
            const int columnIndex = 0;
            const int taskPriority = 0;
            const string taskName = "taskName";

            Assert.True(
                board.GetPrioritedTaskIndexFromColumnBy(columnIndex, taskPriority, taskName) == -1);
        }

        [Fact]
        public void Moving_column_changing_columns_index()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            for (int i = 0; i < 2; ++i)
            {
                board.AddColumn(columnName + i.ToString());
            }

            board.MoveColumnFromTo(0, 1);
            Assert.True(board.GetColumnIndexByName(columnName + "0") == 1);
            Assert.True(board.GetColumnIndexByName(columnName + "1") == 0);
        }

        [Fact]
        public void Attempt_moving_columns_with_wrong_parametrs_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            for (int i = 0; i < 8; ++i)
            {
                board.AddColumn(columnName + i.ToString());
            }

            Assert.Throws<Exception>(() => board.MoveColumnFromTo(-10, 8));
            Assert.Throws<Exception>(() => board.MoveColumnFromTo(0, 88));
            Assert.Throws<Exception>(() => board.MoveColumnFromTo(0, 9));
        }

        [Fact]
        public void Renaming_column_renames_column()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string newColumnName = "ColumnNew";
            board.AddColumn(columnName);

            board.RenameColumn(columnName, newColumnName);

            Assert.True(board.GetColumnNameByIndex(0) == newColumnName);
        }

        [Fact]
        public void Renaming_column_with_wrong_parametrs_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string newColumnName = "ColumnNew";
            board.AddColumn(columnName);

            Assert.Throws<Exception>(() => board.RenameColumn("", newColumnName));
            Assert.Throws<Exception>(() => board.RenameColumn(columnName, ""));
        }

        [Fact]
        public void Renaming_non_existing_column_gives_no_effect()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            const string newColumnName = "ColumnNew";
            board.AddColumn(columnName);

            board.RenameColumn("123", newColumnName);

            Assert.True(board.GetColumnNameByIndex(0) == columnName);
        }

        [Fact]
        public void Removing_column_removes_column()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            board.AddColumn(columnName);

            board.RemoveColumn(columnName);

            Assert.True(board.GetColumnAmount() == 0);
            Assert.True(board.GetAllColumnsNames().Count == 0);
        }

        [Fact]
        public void Removing_columns_with_wrong_parametrs_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            board.AddColumn(columnName);

            Assert.Throws<Exception>(() => board.RemoveColumn(""));
            Assert.Throws<Exception>(() => board.RemoveColumn("Column123"));
            Assert.True(board.GetColumnAmount() == 1);
            Assert.True(board.GetAllColumnsNames().Count == 1);
            Assert.True(board.GetColumnNameByIndex(0) == columnName);
            Assert.True(board.GetAllColumnsNames()[0] == columnName);
        }

        [Fact]
        public void Removing_task_from_column_removes_task()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            board.AddColumn(columnName);
            const string taskName = "Task1";
            const string taskDescription = "Task1's description";
            const int taskPriority = 10;
            int columnIndex = board.GetColumnIndexByName(columnName);
            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, columnIndex);

            board.RemoveTaskFromColumn(columnName, taskPriority, taskName);

            Assert.True(board.GetPrioritedTaskIndexFromColumnBy(columnIndex, taskPriority, taskName) == -1);
        }

        [Fact]
        public void Removing_task_from_column_with_wrong_parameters_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnName = "Column";
            board.AddColumn(columnName);
            const string taskName = "Task1";
            const string taskDescription = "Task1's description";
            const int taskPriority = 10;
            int columnIndex = board.GetColumnIndexByName(columnName);
            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, columnIndex);

            Assert.Throws<Exception>(
                () => board.RemoveTaskFromColumn("", taskPriority, taskName));
            Assert.Throws<Exception>(
                () => board.RemoveTaskFromColumn(columnName, -100, taskName));
            Assert.Throws<Exception>(
                () => board.RemoveTaskFromColumn(columnName, taskPriority, ""));

        }

        [Fact]
        public void Moving_task_from_column_to_column_moves_task()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnNameFirst = "FirstColumn";
            const string columnNameSecond = "SecondColumn";
            board.AddColumn(columnNameFirst);
            board.AddColumn(columnNameSecond);
            const string taskName = "Task1";
            const string taskDescription = "Task1's description";
            const int taskPriority = 10;
            int firstColumnIndex = board.GetColumnIndexByName(columnNameFirst);
            int secondColumnIndex = board.GetColumnIndexByName(columnNameSecond);
            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, firstColumnIndex);

            board.MoveTaskToAnotherColumn(columnNameFirst, columnNameSecond, taskPriority, taskName);

            Assert.True(board.GetPrioritedTaskIndexFromColumnBy(firstColumnIndex, taskPriority, taskName) == -1);
            Assert.True(board.GetPrioritedTaskIndexFromColumnBy(secondColumnIndex, taskPriority, taskName) == 0);
        }

        [Fact]
        public void Moving_task_from_column_to_clumn_with_wrong_parameters_fails()
        {
            IBoard board = MockBoard("TestBoard");
            const string columnNameFirst = "FirstColumn";
            const string columnNameSecond = "SecondColumn";
            board.AddColumn(columnNameFirst);
            board.AddColumn(columnNameSecond);
            const string taskName = "Task1";
            const string taskDescription = "Task1's description";
            const int taskPriority = 10;
            int firstColumnIndex = board.GetColumnIndexByName(columnNameFirst);
            int secondColumnIndex = board.GetColumnIndexByName(columnNameSecond);
            board.AddTaskIntoColumn(taskName, taskDescription, taskPriority, firstColumnIndex);

            Assert.Throws<Exception>(() => board.MoveTaskToAnotherColumn("", columnNameSecond, taskPriority, taskName));
            Assert.Throws<Exception>(() => board.MoveTaskToAnotherColumn(columnNameFirst, "", taskPriority, taskName));
            Assert.Throws<Exception>(() => board.MoveTaskToAnotherColumn(columnNameFirst, columnNameSecond, -500, taskName));
            Assert.Throws<Exception>(() => board.MoveTaskToAnotherColumn(columnNameFirst, columnNameSecond, taskPriority, ""));
        }

        private IBoard MockBoard(string title)
        {
            return ScrumBoardFactory.CreateBoard(title);
        }
    }
}
