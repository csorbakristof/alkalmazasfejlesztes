using Environment;
using System;
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

        /// <summary>
        /// Helper method for bool sensor status updates. Fires event is status has changed.
        /// </summary>
        /// <param name="newStatus">New sensor status</param>
        /// <param name="previousStatus">Reference to variable storing previous value</param>
        /// <param name="onTrueEvent">Event to fire if status changed to true.</param>
        /// <param name="onFalseEvent">Event to fire if status changed to false.</param>
        private void UpdateSensorStatus(bool newStatus, ref bool? previousStatus,
            SensorStatusChangeDelegate onTrueEvent, SensorStatusChangeDelegate onFalseEvent)
        {
            if (!previousStatus.HasValue || previousStatus != newStatus)
            {
                if (newStatus)
                    onTrueEvent?.Invoke();
                else
                    onFalseEvent?.Invoke();
                previousStatus = newStatus;
            }
        }

        #region Polling sensors
        private bool? lastLineStatus = null;
        private void PollLineSensor()
        {
            UpdateSensorStatus(LineSensor.Scan().Max() > 0,
                ref lastLineStatus, OnLineAppears, OnLineDisappears);
        }

        private bool? lastLeftWallStatus = null;
        private void PollLeftWallSensor()
        {
            double leftDistance = LeftWallSensor.GetDistance(-90.0, 10, 20);
            UpdateSensorStatus(leftDistance < 20,
                ref lastLeftWallStatus, OnWallOnLeft, OnNoWallOnLeft);
        }

        private bool? lastRightWallStatus = null;
        private void PollRightWallSensor()
        {
            double rightDistance = LeftWallSensor.GetDistance(90.0, 10, 20);
            UpdateSensorStatus(rightDistance < 20,
                ref lastRightWallStatus, OnWallOnRight, OnNoWallOnRight);
        }
        #endregion

        #region Events indicating sensed environment changes
        public SensorStatusChangeDelegate OnLineAppears;
        public SensorStatusChangeDelegate OnLineDisappears;
        public SensorStatusChangeDelegate OnWallOnLeft;
        public SensorStatusChangeDelegate OnNoWallOnLeft;
        public SensorStatusChangeDelegate OnWallOnRight;
        public SensorStatusChangeDelegate OnNoWallOnRight;

        public delegate void SensorStatusChangeDelegate();
        #endregion
    }
}
