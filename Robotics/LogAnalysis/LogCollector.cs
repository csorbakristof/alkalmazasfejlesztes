using RobotBrain;
using RobotBrain.LogEntry;

namespace LogAnalysis
{
    public class LogCollector
    {
        private readonly IBrain brain;
        private readonly ILogEntryVisitor visitor;

        public LogCollector(IBrain brain, ILogEntryVisitor visitor)
        {
            this.brain = brain;
            this.visitor = visitor;
            brain.OnLoggedEvent += Brain_OnLoggedEvent;
        }

        public IBrain Brain => brain;

        private void Brain_OnLoggedEvent(ILogEntry newEntry)
        {
            // According to Visitor design pattern, we use double dispatch
            //  and ask newEntry to call the visitor.
            newEntry.Accept(visitor);
        }
    }
}
