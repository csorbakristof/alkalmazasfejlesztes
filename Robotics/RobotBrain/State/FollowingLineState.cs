using Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class FollowingLineState : StateBase
    {
        // This state requires a LineAndWallDetectorRobot.
        LineAndWallDetectorRobot Robot => this.Brain.Robot as LineAndWallDetectorRobot;

        private readonly double targetSpeed;

        public FollowingLineState(double targetSpeed = 1.0)
        {
            this.targetSpeed = targetSpeed;
        }

        public override void Tick()
        {
            base.Tick();

            if (this.Brain.Robot.Speed < targetSpeed)
                this.Brain.Robot.Acceleration = 1.0;
            else
                this.Brain.Robot.Acceleration = 0.0;

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
