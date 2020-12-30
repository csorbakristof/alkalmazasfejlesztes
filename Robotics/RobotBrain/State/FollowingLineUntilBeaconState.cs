using Robot;

namespace RobotBrain.State
{
    [System.Obsolete("Used only temporarily in the lab. Use UntilBeaconDecorator instead.")]
    public class FollowingLineUntilBeaconState : StateBase
    {
        // This state requires a LineAndWallDetectorRobot.
        LineAndWallDetectorRobot Robot => this.Brain.Robot as LineAndWallDetectorRobot;

        private readonly double targetSpeed;

        private readonly IState followerState;
        private int beaconID;

        public FollowingLineUntilBeaconState(double targetSpeed, IState follower, int beaconID)
        {
            this.targetSpeed = targetSpeed;
            this.followerState = follower;
            this.beaconID = beaconID;
        }

        public override void OnBeaconClose(int id)
        {
            base.OnBeaconClose(id);
            if (id == this.beaconID)
                this.Brain.CurrentState = followerState;
        }

        public override void Tick()
        {
            base.Tick();

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
