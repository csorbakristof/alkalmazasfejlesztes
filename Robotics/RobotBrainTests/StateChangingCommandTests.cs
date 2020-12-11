using LogAnalysis;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class StateChangingCommandTests : BrainTestBase
    {
        [Fact]
        public void StateChangeLogEntryTriggeringTest()
        {
            var visitor = RegisterTestLogVisitor(typeof(SleepState));
            brain.CurrentState= new SleepState(10);
            Assert.True(visitor.DidEnterCheckedState);
        }

        [Fact]
        public void TestDetectingSequenceOfStateChanges()
        {
            var stateA = new SleepState(2);
            var stateB = new TurnState(0.0, 1.0);

            var sleepCheckerVisitor = RegisterTestLogVisitor(typeof(SleepState));
            var turnCheckerVisitor = RegisterTestLogVisitor(typeof(TurnState));

            brain.CurrentState = stateA;
            for (int i = 0; i < 2; i++)
                env.Tick();
            brain.CurrentState = stateB;

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
            checker.IgnoreIdleState = true;
            checker.StateTypeSequence.Enqueue(stateA.GetType());
            checker.StateTypeSequence.Enqueue(stateB.GetType());
            Assert.False(checker.AreAllStatesVisited());
            brain.CurrentState = stateA;
            WaitTicks(2);
            Assert.False(checker.AreAllStatesVisited());
            brain.CurrentState = stateB;
            WaitTicks(3);
            Assert.True(checker.AreAllStatesVisited());

            brain.CurrentState = stateA;
            WaitTicks(2);
            Assert.False(checker.AreAllStatesVisited());
        }

        private void WaitTicks(int ticks)
        {
            for (int i = 0; i < ticks; i++)
                env.Tick();
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

            brain.CurrentState = stateB;
            brain.CurrentState = stateA;
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
