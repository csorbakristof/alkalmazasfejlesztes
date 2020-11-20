using RobotBrain.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotBrainTests
{
    public class SquareCommandTest
    {
        [Fact]
        public void Basics()
        {
            var cmd = new SquareCommand();
        }
    }
}
