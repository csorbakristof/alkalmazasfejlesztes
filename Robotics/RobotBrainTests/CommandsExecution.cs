using EnvironmentSimulator;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class CommandsExecution : BrainTestBase
    {
        [Fact]
        public void RunNopCommand()
        {
            brain.AddCommand(new NopCommand());
            sim.Tick();
            Assert.NotNull(lastLogEntry);
            Assert.True(lastLogEntry is CommandCompleteLogEntry);
        }

        [Fact]
        public void RunSleepCommand()
        {
            brain.AddCommand(new GenericSingleStateCommand(new SleepState(2)));
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.Null(lastLogEntry);
            sim.Tick();
            Assert.True(lastLogEntry is CommandCompleteLogEntry);
        }

        [Fact]
        public void NextStateDecorator()
        {
            var turnAwayState = new TurnState(10.0, 1.0);
            var turnBackState = new TurnState(0.0, -1.0);
            // Note: without the Then extension method, we would have to write this:
            //  var turnAwayAndThenBackState = new AfterIdleStateDecorator(turnAwayState, turnBackState);
            var turnAwayAndThenBackState = turnAwayState.Then(turnBackState);

            brain.AddCommand(new GenericSingleStateCommand(turnAwayAndThenBackState));
            Assert.Equal(0.0, sim.Direction, 1);
            for (int i = 0; i < 10; i++)
                sim.Tick();
            Assert.Equal(10.0, sim.Direction, 1);
            // Now the decorator should have modified the transition to IdleState to a transition
            //  to turnBackState.
            for (int i = 0; i < 10; i++)
                sim.Tick();
            Assert.Equal(0.0, sim.Direction, 1);
        }
    }
}
