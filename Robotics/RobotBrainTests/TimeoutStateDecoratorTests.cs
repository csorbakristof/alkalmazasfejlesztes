using Environment;
using Robot;
using RobotBrain;
using RobotBrain.State;
using Xunit;

namespace RobotBrainTests
{
    public class TimeoutStateDecoratorTests
    {
        [Fact]
        public void TimeoutChangesStateToFollower()
        {
            var brain = new BrainBase(new RobotBase(new DefaultEnvironment(new Map(10,10))));
            var decoratedState = new IdleState();
            var followerState = new IdleState();
            var state = new TimeoutStateDecorator(decoratedState, 3, followerState);
            brain.CurrentState = state;
            Assert.Equal(state, brain.CurrentState);
            state.Tick();
            Assert.Equal(state, brain.CurrentState);
            state.Tick();
            Assert.Equal(state, brain.CurrentState);
            state.Tick();
            Assert.Equal(followerState, brain.CurrentState);
            state.Tick();
        }
    }
}
