using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.LogEntry
{
    public class LogEntryVisitorBase : ILogEntryVisitor
    {
        public virtual void Visit(StateChangeLogEntry logEntry) { }

        public virtual void Visit(GenericLogEntry logEntry) { }

        public virtual void Visit(TickLogEntry logEntry) { }

        public virtual void Visit(RobotEventLogEntry logEntry) { }
    }
}
