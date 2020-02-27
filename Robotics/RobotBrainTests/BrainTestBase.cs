using Environment;
using Robot;
using RobotBrain;
using RobotBrain.LogEntry;

namespace RobotBrainTests
{
    public class BrainTestBase
    {
        readonly protected IEnvironment env;
        readonly protected IRobot robot;
        readonly protected IBrain brain;
        protected ILogEntry lastLogEntry = null;

        public BrainTestBase()
        {
            env = new DefaultEnvironment(null);
            robot = new RobotBase(env);
            brain = new BrainBase(robot);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }
    }
}
