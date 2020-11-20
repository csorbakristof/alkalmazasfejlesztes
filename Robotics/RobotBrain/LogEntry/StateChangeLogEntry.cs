using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.LogEntry
{
    public class StateChangeLogEntry : ILogEntry
    {
        public IState NewState { get; set; }
        public StateChangeLogEntry(IState newState)
        {
            this.NewState = newState;
        }

        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
