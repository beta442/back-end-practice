using Xunit;

using ScrumBoard.Board;

namespace ScrumBoardTest
{
    public class BoardUnitTest
    {
        [Fact]
        public void Created_board_named_correctly()
        {
            const string boardName = "TestBoardName";
            Board board = new (boardName);

            Assert.True(board.GetBoardName() == boardName);
        }

        [Fact]
        public void Adding_column_into_board_creates_column_in_board()
        {
            Board board = new ("TestBoard");
            const string columnName = "TestColumn";

            board.AddColumn(columnName);

            Assert.True(board.GetColumnIndexByName(columnName) != -1);
        }

        [Fact]
        public void Adding_two_columns_with_equal_names_fails()
        {
            Board board = new("TestBoard");
            const string columnName = "TestColumn";

            for (int i = 0; i < 2; ++i)
            {
                board.AddColumn(columnName);
            }

            Assert.True(board.GetAllColumnsNames().Count == 1);
            Assert.True(board.GetAllColumnsNames()[0] == columnName);
        }

        [Fact]
        public void Adding_column_with_empty_name_gives_no_effect()
        {
            Board board = new("TestBoard");
            const string columnName = "";

            board.AddColumn(columnName);

            Assert.True(board.GetAllColumnsNames().Count == 0);
        }

        [Fact]
        public void Adding_column_when_board_has_more_than_10_columns_gives_no_effect()
        {
            Board board = new("TestBoard");
            const string columnName = "Column";

            for (int i = 0; i < 10; ++i)
            {
                board.AddColumn(columnName + i.ToString());
            }
            board.AddColumn(columnName + "10");

            Assert.True(board.GetAllColumnsNames().Count == 10);
            Assert.True(!board.GetAllColumnsNames().Contains(columnName + "10"));
        }

        [Fact]
        public void Moving_column_changing_columns_index()
        {
            Board board = new("TestBoard");
            const string columnName = "Column";
            for (int i = 0; i < 4; ++i)
            {
                board.AddColumn(columnName + i.ToString());
            }

            board.MoveColumnFromTo();
        }
    }
}
