using Robot;

namespace RobotBrain.State
{
    public class FollowingLineWithTimeoutState : StateBase
    {
        // This state requires a LineAndWallDetectorRobot.
        LineAndWallDetectorRobot Robot => this.Brain.Robot as LineAndWallDetectorRobot;

        private readonly double targetSpeed;

        private readonly IState followerState;
        private int timeout;

        public FollowingLineWithTimeoutState(double targetSpeed, IState follower, int timeout)
        {
            this.targetSpeed = targetSpeed;
            this.followerState = follower;
            this.timeout = timeout;
        }

        public override void Tick()
        {
            base.Tick();

            // TASK: added timeout functionality
            timeout--;
            if (timeout <= 0)
                this.Brain.CurrentState = followerState;

            if (this.Brain.Robot.Speed < targetSpeed)
                this.Brain.Robot.Acceleration = 1.0;
            else
                this.Brain.Robot.Acceleration = -0.1;

            int[] scan = Robot.LineSensor.Scan();
            int middleIndex = (scan.Length / 2) + 1;
            int sumLeft = 0;
            int sumRight = 0;
            for (int i = 0; i < scan.Length; i++)
            {
                if (scan[i] > 0)
                {
                    if (i < middleIndex)
                        sumLeft++;
                    else if (i > middleIndex)
                        sumRight++;
                }
            }
            if (sumLeft > sumRight)
                Robot.Turn = -10.0;
            else if (sumLeft < sumRight)
                Robot.Turn = 10.0;
            else if (sumLeft+sumRight > 0)
                Robot.Turn = 0.0;
            // If line is entirely lost, keep turning.
        }
    }
}
