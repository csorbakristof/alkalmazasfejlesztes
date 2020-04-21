using Robot;

namespace RobotBrain.State
{
    public abstract class DistanceBasedTurningStateBase : StateBase
    {
        // This state requires a LineAndWallDetectorRobot.
        protected LineAndWallDetectorRobot Robot => this.Brain.Robot as LineAndWallDetectorRobot;

        public override void Tick()
        {
            base.Tick();
            AccelerateIfStopped();

            double distance = GetDistance();
            if (distance < Robot.WallSensorMaxDistance)
            {
                if (distance < TooSmallDistanceThreshold)
                    Robot.Turn = TurnValueOnTooSmallDistance;
                else if (distance > TooHighDistanceThreshold)
                    Robot.Turn = TurnValueOnTooHighDistance;
                else
                    Robot.Turn = 0.0;
            }
            else
            {
                // Wall lost, turning
                Robot.Turn = TurnValueOnMaximalDistance;
            }
        }

        abstract protected double GetDistance();
        protected double TargetSpeed = 5.0;
        protected double TooSmallDistanceThreshold = 20;
        protected double TurnValueOnTooSmallDistance;
        public double TooHighDistanceThreshold = 70;
        protected double TurnValueOnTooHighDistance;
        protected double TurnValueOnMaximalDistance;

        protected void AccelerateIfStopped()
        {
            if (this.Brain.Robot.Speed < TargetSpeed)
                this.Brain.Robot.Acceleration = 1.0;
            else
                this.Brain.Robot.Acceleration = 0.0;
        }
    }
}
