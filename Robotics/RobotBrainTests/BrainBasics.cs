using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.LogEntry;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class BrainBasics
    {
        [Fact]
        public void Instantiation()
        {
            ISimulator sim = new DefaultSimulator();
            IBrain brain = new DefaultBrain(sim);
            ILogEntry newEntry = null;
            brain.OnLoggedEvent += (ILogEntry entry) => newEntry = entry;
            // Now simulate a change outside the brain and expect log entry about it.
            sim.Direction = 1.0;
            Assert.NotNull(newEntry);
        }
    }
}
