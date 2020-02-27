using Environment;
using System.Linq;

namespace Robot
{
    /// <summary>
    /// Class of the actual robot with sensors for wall and line following.
    /// On the map, 0 is empty, 1-9 is line, 10+ is wall.
    /// </summary>
    public class FollowerRobot : RobotBase
    {
        public LineSensor LineSensor { get; set; }
        public DistanceSensor LeftWallSensor { get; private set; }
        public DistanceSensor RightWallSensor { get; private set; }

        public FollowerRobot(IEnvironment env) : base(env)
        {
            LineSensor = new LineSensor(this);
            LeftWallSensor = new DistanceSensor(this);
            RightWallSensor = new DistanceSensor(this);
        }

        protected override void CheckSensorValuesAndFireEvents()
        {
            base.CheckSensorValuesAndFireEvents();
            PollLineSensor();
            PollLeftWallSensor();
            PollRightWallSensor();
        }

        #region Polling sensors
        private void PollLineSensor()
        {
            int[] values = LineSensor.Scan();
            if (values.Max() > 0)
                OnLineAppears?.Invoke();
            else
                OnLineDisappears?.Invoke();
        }

        private void PollLeftWallSensor()
        {
            double leftDistance = LeftWallSensor.GetDistance(-90.0, 10, 20);
            if (leftDistance < 20)
                OnWallOnLeft?.Invoke();
            else
                OnNoWallOnLeft?.Invoke();
        }

        private void PollRightWallSensor()
        {
            double rightDistance = LeftWallSensor.GetDistance(90.0, 10, 20);
            if (rightDistance < 20)
                OnWallOnRight?.Invoke();
            else
                OnNoWallOnRight?.Invoke();
        }
        #endregion

        #region Events indicating sensed environment changes
        public OnLineAppearsDelegate OnLineAppears;
        public OnLineDisappearsDelegate OnLineDisappears;
        public OnWallOnLeftDelegate OnWallOnLeft;
        public OnNoWallOnLeftDelegate OnNoWallOnLeft;
        public OnWallOnRightDelegate OnWallOnRight;
        public OnNoWallOnRightDelegate OnNoWallOnRight;

        public delegate void OnLineAppearsDelegate();
        public delegate void OnLineDisappearsDelegate();
        public delegate void OnWallOnLeftDelegate();
        public delegate void OnNoWallOnLeftDelegate();
        public delegate void OnWallOnRightDelegate();
        public delegate void OnNoWallOnRightDelegate();
        #endregion
    }
}
