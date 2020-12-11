using RobotBrain.State;
using Xunit;

namespace RobotBrainTests
{
    public class NextStateDecoratorTests : BrainTestBase
    {
        [Fact]
        public void NextStateDecorator()
        {
            var turnAwayState = new TurnState(10.0, 1.0);
            var turnBackState = new TurnState(0.0, -1.0);
            // Note: without the Then extension method, we would have to write this:
            //  var turnAwayAndThenBackState = new AfterIdleStateDecorator(turnAwayState, turnBackState);
            var turnAwayAndThenBackState = turnAwayState.Then(turnBackState);

            brain.CurrentState = turnAwayAndThenBackState;
            Assert.Equal(0.0, robot.Orientation, 1);
            for (int i = 0; i < 10; i++)
                env.Tick();
            Assert.Equal(10.0, robot.Orientation, 1);
            // Now the decorator should have modified the transition to IdleState to a transition
            //  to turnBackState.
            for (int i = 0; i < 10; i++)
                env.Tick();
            Assert.Equal(0.0, robot.Orientation, 1);
        }
    }
}
