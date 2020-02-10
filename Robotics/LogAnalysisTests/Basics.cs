using EnvironmentSimulator;
using LogAnalysis;
using RobotBrain;
using RobotBrain.LogEntry;
using Xunit;

namespace LogAnalysisTests
{
    public class Basics
    {
        readonly protected IEnvironment sim;
        readonly protected IBrain brain;
        protected ILogEntry lastLogEntry = null;

        public Basics()
        {
            sim = new DefaultSimulator(null);
            brain = new DefaultBrain(sim);
            brain.OnLoggedEvent += (ILogEntry entry) => lastLogEntry = entry;
        }


        [Fact]
        public void Instantiation()
        {
            var visitor = new TestVisitor();
            new LogCollector(brain, visitor);
            Assert.Equal(0.0, sim.Direction, 1);
            // Direction change will fire a log event which is captured by LogCollector
            //  and forwarded to TestVisitor.
            sim.Direction = 1.0;
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
