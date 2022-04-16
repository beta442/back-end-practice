using Xunit;

using ScrumBoard.TaskColumn;
using ScrumBoard.Factory;
using System;

namespace ScrumBoardTest
{
    public class TaskColumnUnitTest
    {
        [Fact]
        public void C()
        {
            ITaskColumn taskColumn = MockColumn("Hey");
        }

        private ITaskColumn MockColumn(string title)
        {
            return ScrumBoardFactory.CreateColumn(title);
        }
    }
}
