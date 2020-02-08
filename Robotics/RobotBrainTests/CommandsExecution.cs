using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class CommandsExecution
    {
        readonly ISimulator sim;
        readonly IBrain brain;
        ILogEntry lastLogEntry = null;

        public CommandsExecution()
        {
            sim = new DefaultSimulator();
            brain = new DefaultBrain(sim);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }

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
            brain.AddCommand(new SleepCommand(2));
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.True(lastLogEntry is CommandComplete);
        }
    }
}
