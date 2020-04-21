using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotBrainTests
{
    public class BasicMotionCommands : BrainTestBase
    {
        [Fact]
        public void TurnCommand()
        {
            Assert.True(brain.CurrentState is IdleState);
            Assert.Equal(0.0, robot.Orientation, 1);
            brain.AddCommand(new GenericSingleStateCommand(new TurnState(10.0, 1.0)));
            Assert.False(lastLogEntry is CommandCompleteLogEntry);    // Note: it will be GenericLogEntry
            for(int i=0; i<10; i++)
                env.Tick();
            Assert.True(lastLogEntry is CommandCompleteLogEntry);

        }
    }
}
