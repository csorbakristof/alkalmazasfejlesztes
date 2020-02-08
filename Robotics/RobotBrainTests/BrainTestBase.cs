using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.LogEntry;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrainTests
{
    public class BrainTestBase
    {
        readonly protected IEnvironment sim;
        readonly protected IBrain brain;
        protected ILogEntry lastLogEntry = null;

        public BrainTestBase()
        {
            sim = new DefaultSimulator();
            brain = new DefaultBrain(sim);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }
    }
}
