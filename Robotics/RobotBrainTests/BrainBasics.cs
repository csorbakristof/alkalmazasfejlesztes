using Environment;
using RobotBrain;
using RobotBrain.LogEntry;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class BrainBasics : BrainTestBase
    {
        [Fact]
        public void Instantiation()
        {
            // Now simulate a change outside the brain and expect log entry about it.
            robot.Direction = 1.0;
            Assert.NotNull(lastLogEntry);
        }
    }
}
