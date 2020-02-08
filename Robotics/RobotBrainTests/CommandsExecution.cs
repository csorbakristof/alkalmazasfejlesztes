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
        [Fact]
        public void RunNopCommand()
        {
            ISimulator sim = new DefaultSimulator();
            IBrain brain = new DefaultBrain(sim);
            ILogEntry newEntry = null;
            brain.OnLoggedEvent += (ILogEntry entry) => newEntry = entry;

            ICommand cmd = new NopCommand();
            brain.AddCommand(cmd);
            sim.Tick();
            Assert.NotNull(newEntry);
            Assert.True(newEntry is CommandComplete);
        }

        [Fact]
        public void RunSleepCommand()
        {
            ISimulator sim = new DefaultSimulator();
            IBrain brain = new DefaultBrain(sim);
            ILogEntry newEntry = null;
            brain.OnLoggedEvent += (ILogEntry entry) => newEntry = entry;

            ICommand cmd = new SleepCommand(2);
            brain.AddCommand(cmd);
            Assert.Null(newEntry);
            sim.Tick();
            Assert.Null(newEntry);
            sim.Tick();
            Assert.True(newEntry is CommandComplete);
        }
    }
}
