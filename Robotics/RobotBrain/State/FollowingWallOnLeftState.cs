using Robot;

namespace RobotBrain.State
{
    public class FollowingWallOnLeftState : DistanceBasedTurningStateBase
    {
        protected override double GetDistance() => Robot.LeftWallSensor.GetDistance();
        public FollowingWallOnLeftState() : base()
        {
            TurnValueOnTooSmallDistance = 5.0;
            TurnValueOnTooHighDistance = -5.0;
            TurnValueOnMaximalDistance = -5.0;
        }
    }
}
