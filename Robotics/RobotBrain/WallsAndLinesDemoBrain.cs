using System;
using System.Collections.Generic;
using System.Text;
using Robot;

namespace RobotBrain
{
    public class WallsAndLinesDemoBrain : BrainBase
    {
        public WallsAndLinesDemoBrain(LineAndWallDetectorRobot robot) : base(robot)
        {
            robot.OnLineAppears += Robot_OnLineAppears;
            robot.OnLineDisappears += Robot_OnLineDisappears;
            robot.OnWallOnLeft += Robot_OnWallOnLeft;
            robot.OnNoWallOnLeft += Robot_OnNoWallOnLeft;
            robot.OnWallOnRight += Robot_OnWallOnRight;
            robot.OnNoWallOnRight += Robot_OnNoWallOnRight;
        }

        #region Event forwarding to CurrentState
        private void Robot_OnNoWallOnRight()
        {
            CurrentState.OnNoWallOnRight();

        }

        private void Robot_OnWallOnRight()
        {
            CurrentState.OnWallOnRight();
        }

        private void Robot_OnNoWallOnLeft()
        {
            CurrentState.OnNoWallOnLeft();
        }

        private void Robot_OnWallOnLeft()
        {
            CurrentState.OnWallOnLeft();
        }

        private void Robot_OnLineDisappears()
        {
            CurrentState.OnLineDisappears();
        }

        private void Robot_OnLineAppears()
        {
            CurrentState.OnLineAppears();
        }
        #endregion
    }
}
