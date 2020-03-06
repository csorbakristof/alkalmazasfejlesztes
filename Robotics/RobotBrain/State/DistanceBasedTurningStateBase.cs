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
                if (distance > 10.0)
                    Robot.Turn = TurnValueOnTooHighDistance;
                else
                    Robot.Turn = TurnValueOnTooSmallDistance;
            }
            else
            {
                // Wall lost, turning
                Robot.Turn = TurnValueOnMaximalDistance;
            }
        }

        abstract protected double GetDistance();
        protected double TurnValueOnTooSmallDistance;
        protected double TurnValueOnTooHighDistance;
        protected double TurnValueOnMaximalDistance;

        protected void AccelerateIfStopped()
        {
            if (this.Brain.Robot.Speed < 1.0)
                this.Brain.Robot.Acceleration = 1.0;
            else
                this.Brain.Robot.Acceleration = 0.0;
        }
    }
}
