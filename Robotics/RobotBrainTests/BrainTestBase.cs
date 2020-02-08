using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.LogEntry;

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
