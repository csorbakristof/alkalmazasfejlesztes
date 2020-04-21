using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.LogEntry
{
    public class RobotEventLogEntry : ILogEntry
    {
        public string EventName { get; set; }

        public RobotEventLogEntry(string eventName)
        {
            this.EventName = eventName;
        }

        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
