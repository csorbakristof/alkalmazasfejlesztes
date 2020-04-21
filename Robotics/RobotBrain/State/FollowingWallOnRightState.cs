using Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class FollowingWallOnRightState : DistanceBasedTurningStateBase
    {
        protected override double GetDistance() => Robot.RightWallSensor.GetDistance();
        public FollowingWallOnRightState(double turnSpeed = 10, double minDistanceThreshold = 20) : base()
        {
            TurnValueOnTooSmallDistance = -turnSpeed;
            TurnValueOnTooHighDistance = turnSpeed;
            TurnValueOnMaximalDistance = turnSpeed;
            TooSmallDistanceThreshold = minDistanceThreshold;
        }
    }
}
