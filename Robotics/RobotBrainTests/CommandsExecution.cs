using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class CommandsExecution : BrainTestBase
    {
        [Fact]
        public void RunNopCommand()
        {
            brain.AddCommand(new NopCommand());
            sim.Tick();
            Assert.NotNull(lastLogEntry);
            Assert.True(lastLogEntry is CommandComplete);
        }

        [Fact]
        public void RunSleepCommand()
        {
            brain.AddCommand(new GenericSingleStateCommand(new SleepState(2)));
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.True(lastLogEntry is CommandComplete);
        }
    }
}
