using System;
using System.Collections.Generic;
using System.Text;
using Environment;
using Robot;
using RobotBrain;
using RobotBrain.State;
using Xunit;

namespace RobotBrainTests
{
    public class WallsAndLinesDemoBrainTests : Robot.IRobot
    {
        private WallsAndLinesDemoBrain brain;

        public WallsAndLinesDemoBrainTests()
        {
            this.Environment = new DefaultEnvironment(map);
            brain = new WallsAndLinesDemoBrain(this);
        }

        #region Simulating the IRobot interface
        private Map map = new Map(100,100);

        public LocOri LocationOrientation { get; set; }
        public Point Location { get; set; }
        public double Orientation { get; set; }
        public double Speed { get; set; }
        public double Turn { get; set; }
        public double Acceleration { get; set; }
        public IEnvironment Environment { get; set; }

        public event Robot.OnTickDelegate OnTick;
        #endregion

        // To test: proper state transitions upon signals from robot
        [Fact]
        public void InitialState()
        {
            Assert.True(brain.CurrentState is IdleState);
        }

    }
}
