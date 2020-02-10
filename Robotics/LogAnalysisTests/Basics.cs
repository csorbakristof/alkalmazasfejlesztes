using Environment;
using LogAnalysis;
using Robot;
using RobotBrain;
using RobotBrain.LogEntry;
using Xunit;

namespace LogAnalysisTests
{
    public class Basics
    {
        readonly protected IEnvironment env;
        readonly protected IRobot robot;
        readonly protected IBrain brain;
        protected ILogEntry lastLogEntry = null;

        public Basics()
        {
            env = new DefaultEnvironment(null);
            robot = new DefaultRobot(env);
            brain = new DefaultBrain(robot);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }


        [Fact]
        public void Instantiation()
        {
            var visitor = new TestVisitor();
            new LogCollector(brain, visitor);
            Assert.Equal(0.0, robot.Direction, 1);
            // Direction change will fire a log event which is captured by LogCollector
            //  and forwarded to TestVisitor.
            robot.Direction = 1.0;
            Assert.True(visitor.Visited);
        }

        internal class TestVisitor : ILogEntryVisitor
        {
            public bool Visited = false;

            public void Visit(CommandCompleteLogEntry logEntry)
            {
            }

            public void Visit(GenericLogEntry logEntry)
            {
                Visited = true;
            }
        }
    }
}
