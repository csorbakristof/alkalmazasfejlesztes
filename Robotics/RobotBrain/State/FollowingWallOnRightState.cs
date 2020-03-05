using Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class FollowingWallOnRightState : DistanceBasedTurningStateBase
    {
        protected override double GetDistance() => Robot.RightWallSensor.GetDistance();
        public FollowingWallOnRightState() : base()
        {
            TurnValueOnTooSmallDistance = -5.0;
            TurnValueOnTooHighDistance = 5.0;
            TurnValueOnMaximalDistance = 5.0;
        }
    }
}
