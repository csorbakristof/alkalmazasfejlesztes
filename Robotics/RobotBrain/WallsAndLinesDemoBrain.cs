using System.Runtime.CompilerServices;
using Robot;
using RobotBrain.LogEntry;

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
            LogCall();
            CurrentState.OnNoWallOnRight();
        }

        private void Robot_OnWallOnRight()
        {
            LogCall();
            CurrentState.OnWallOnRight();
        }

        private void Robot_OnNoWallOnLeft()
        {
            LogCall();
            CurrentState.OnNoWallOnLeft();
        }

        private void Robot_OnWallOnLeft()
        {
            LogCall();
            CurrentState.OnWallOnLeft();
        }

        private void Robot_OnLineDisappears()
        {
            LogCall();
            CurrentState.OnLineDisappears();
        }

        private void Robot_OnLineAppears()
        {
            LogCall();
            CurrentState.OnLineAppears();
        }
        #endregion

        public void LogCall([CallerMemberName] string caller = null)
        {
            Log(new RobotEventLogEntry(caller));
        }
    }
}
