using Robot;

namespace RobotBrain.State
{
    public class FollowingWallOnLeftState : DistanceBasedTurningStateBase
    {
        protected override double GetDistance() => Robot.LeftWallSensor.GetDistance();
        public FollowingWallOnLeftState(double turnSpeed=10, double minDistanceThreshold=20) : base()
        {
            TurnValueOnTooSmallDistance = turnSpeed;
            TurnValueOnTooHighDistance = -turnSpeed;
            TurnValueOnMaximalDistance = -turnSpeed;
            TooSmallDistanceThreshold = minDistanceThreshold;
        }
    }
}
