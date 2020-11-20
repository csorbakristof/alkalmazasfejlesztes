using LogAnalysis;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotBrainTests
{
    public class StateChangingCommandTests : BrainTestBase
    {
        [Fact]
        public void StateChangeLogEntryTriggeringTest()
        {
            var visitor = registerTestLogVisitor(typeof(SleepState));

            var cmd = new GenericSingleStateCommand(new SleepState(10));
            brain.AddCommand(cmd);
            Assert.True(visitor.DidEnterCheckedState);
        }

        [Fact]
        public void TestDetectingSequenceOfStateChanges()
        {
            var stateA = new SleepState(2);
            var stateB = new TurnState(0.0, 1.0);

            var sleepCheckerVisitor = registerTestLogVisitor(typeof(SleepState));
            var turnCheckerVisitor = registerTestLogVisitor(typeof(TurnState));

            brain.AddCommand(new GenericSingleStateCommand(stateA));
            brain.AddCommand(new GenericSingleStateCommand(stateB));

            Assert.True(sleepCheckerVisitor.DidEnterCheckedState);
            Assert.True(turnCheckerVisitor.DidEnterCheckedState);
        }

        private TestVisitor registerTestLogVisitor(Type stateTypeToCheck)
        {
            var visitor = new TestVisitor(stateTypeToCheck);
            new LogCollector(brain, visitor);
            return visitor;
        }

        class TestVisitor : ILogEntryVisitor
        {
            private Type StateTypeToCheck;
            public bool DidEnterCheckedState { get; private set; } = false;
            public TestVisitor(Type stateTypeToCheck)
            {
                StateTypeToCheck = stateTypeToCheck;
            }

            public void Visit(CommandCompleteLogEntry logEntry)
            {
            }

            public void Visit(StateChangeLogEntry logEntry)
            {
                if (logEntry.NewState.GetType() == StateTypeToCheck)
                    DidEnterCheckedState = true;
            }

            public void Visit(GenericLogEntry logEntry)
            {
            }

            public void Visit(TickLogEntry logEntry)
            {
            }

            public void Visit(RobotEventLogEntry logEntry)
            {
            }
        }
    }
}
