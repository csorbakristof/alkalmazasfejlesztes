using Robot;

namespace RobotBrain.State
{
    public class FollowingWallOnLeftState : DistanceBasedTurningStateBase
    {
        protected override double GetDistance() => Robot.LeftWallSensor.GetDistance();
        public FollowingWallOnLeftState() : base()
        {
            TurnValueOnTooSmallDistance = 10.0;
            TurnValueOnTooHighDistance = -10.0;
            TurnValueOnMaximalDistance = -10.0;
        }
    }
}
