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
            var visitor = RegisterTestLogVisitor(typeof(SleepState));

            var cmd = new GenericSingleStateCommand(new SleepState(10));
            brain.AddCommand(cmd);
            Assert.True(visitor.DidEnterCheckedState);
        }

        [Fact]
        public void TestDetectingSequenceOfStateChanges()
        {
            var stateA = new SleepState(2);
            var stateB = new TurnState(0.0, 1.0);

            var sleepCheckerVisitor = RegisterTestLogVisitor(typeof(SleepState));
            var turnCheckerVisitor = RegisterTestLogVisitor(typeof(TurnState));

            brain.AddCommand(new GenericSingleStateCommand(stateA));
            brain.AddCommand(new GenericSingleStateCommand(stateB));

            Assert.True(sleepCheckerVisitor.DidEnterCheckedState);
            Assert.True(turnCheckerVisitor.DidEnterCheckedState);
        }

        [Fact]
        public void StateSequenceCheckingLogEntryVisitorTest_SuccessfulCase()
        {
            var stateA = new SleepState(2);
            var stateB = new TurnState(0.0, 1.0);
            var checker = new StateSequenceCheckingLogEntryVisitor();
            checker.RegisterAsVisitor(brain);
            checker.StateTypeSequence.Enqueue(stateA.GetType());
            checker.StateTypeSequence.Enqueue(stateB.GetType());
            Assert.False(checker.AreAllStatesVisited());
            brain.AddCommand(new GenericSingleStateCommand(stateA));
            Assert.False(checker.AreAllStatesVisited());
            brain.AddCommand(new GenericSingleStateCommand(stateB));

            Assert.True(checker.AreAllStatesVisited());

            brain.AddCommand(new GenericSingleStateCommand(stateA));
            Assert.False(checker.AreAllStatesVisited());
        }

        [Fact]
        public void StateSequenceCheckingLogEntryVisitorTest_FailingCase()
        {
            var stateA = new SleepState(2);
            var stateB = new TurnState(0.0, 1.0);
            var checker = new StateSequenceCheckingLogEntryVisitor();
            checker.RegisterAsVisitor(brain);
            checker.StateTypeSequence.Enqueue(stateA.GetType());
            checker.StateTypeSequence.Enqueue(stateB.GetType());

            brain.AddCommand(new GenericSingleStateCommand(stateB));
            brain.AddCommand(new GenericSingleStateCommand(stateA));
            Assert.False(checker.AreAllStatesVisited());
        }

        private TestVisitor RegisterTestLogVisitor(Type stateTypeToCheck)
        {
            var visitor = new TestVisitor(stateTypeToCheck);
            new LogCollector(brain, visitor);
            return visitor;
        }

        class TestVisitor : LogEntryVisitorBase
        {
            private readonly Type StateTypeToCheck;
            public bool DidEnterCheckedState { get; private set; } = false;
            public TestVisitor(Type stateTypeToCheck)
            {
                StateTypeToCheck = stateTypeToCheck;
            }

            public override void Visit(StateChangeLogEntry logEntry)
            {
                if (logEntry.NewState.GetType() == StateTypeToCheck)
                    DidEnterCheckedState = true;
            }
        }
    }
}
