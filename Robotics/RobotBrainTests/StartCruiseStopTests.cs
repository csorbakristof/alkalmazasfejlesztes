using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotBrainTests
{
    public class StartCruiseStopTests : BrainTestBase
    {
        [Fact]
        public void CruiseStateTest()
        {
            var state = new CruiseState(5);
            robot.Acceleration = 0.0;
            robot.Speed = 0.0;
            robot.Orientation = 0.0;
            robot.Location = new Environment.Point(0.0, 0.0);
            brain.CurrentState = state;
            env.Tick();
            Assert.NotEqual(0.0, robot.Acceleration, 3);
            env.Tick();
            Assert.NotEqual(0.0, robot.Speed, 3);
            env.Tick();
            Assert.Equal(0.0, robot.Location.X, 3);
            Assert.NotEqual(0.0, robot.Location.Y, 3);
        }

        [Fact]
        public void StopStateTest()
        {
            var state = new StopState();
            robot.Acceleration = 3.0;
            robot.Speed = 2.0;
            brain.CurrentState = state;
            env.Tick();
            Assert.NotEqual(0.0, robot.Acceleration, 3);
            for(int i=0; i<5; i++)
                env.Tick();
            Assert.Equal(0.0, robot.Acceleration, 3);
            Assert.Equal(0.0, robot.Speed, 3);
            Assert.IsType<IdleState>(brain.CurrentState);
        }

    }
}
