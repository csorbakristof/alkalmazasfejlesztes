using LogAnalysis;
using RobotBrain;
using RobotBrain.LogEntry;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrainTests
{
    /// <summary>
    /// Register this LogEntry visitor to check for a given sequence of
    /// state changes (StateChangeLogEntry instances) in unit tests.
    /// </summary>
    class StateSequenceCheckingLogEntryVisitor : LogEntryVisitorBase
    {
        public Queue<Type> StateTypeSequence { get; set; } = new Queue<Type>();
        private bool CheckOkUntilNow = true;
        public bool IgnoreIdleState { get; set; } = false;

        public void RegisterAsVisitor(IBrain brain)
        {
            new LogCollector(brain, this);
        }

        public bool AreAllStatesVisited()
        {
            return CheckOkUntilNow && StateTypeSequence.Count == 0;
        }

        public override void Visit(StateChangeLogEntry logEntry)
        {
            if (logEntry.NewState is RobotBrain.State.IdleState)
                return;

            if (StateTypeSequence.Count > 0)
            {
                var nextAssumedStateType = StateTypeSequence.Dequeue();
                if (logEntry.NewState.GetType() != nextAssumedStateType)
                {
                    CheckOkUntilNow = false;
                }
            }
            else
            {
                CheckOkUntilNow = false;
            }
        }
    }
}
