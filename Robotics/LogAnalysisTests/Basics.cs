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
            robot = new RobotBase(env);
            brain = new BrainBase(robot);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }


        [Fact]
        public void Instantiation()
        {
            var visitor = new TestVisitor();
            new LogCollector(brain, visitor);
            env.Tick();
            // Direction change will fire a log event which is captured by LogCollector
            //  and forwarded to TestVisitor.
            Assert.True(visitor.Visited);
        }

        internal class TestVisitor : ILogEntryVisitor
        {
            public bool Visited = false;

            public void Visit(CommandCompleteLogEntry logEntry)
            {
            }

            public void Visit(TickLogEntry logEntry)
            {
                Visited = true;
            }

            public void Visit(GenericLogEntry logEntry)
            {
            }

            public void Visit(RobotEventLogEntry logEntry)
            {
            }
        }
    }
}
