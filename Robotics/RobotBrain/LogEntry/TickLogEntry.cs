using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.LogEntry
{
    public class TickLogEntry : ILogEntry
    {
        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
