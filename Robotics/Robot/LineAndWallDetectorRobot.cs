using Environment;
using System;
using System.Linq;

namespace Robot
{
    /// <summary>
    /// Class of the actual robot with sensors for wall and line following.
    /// On the map, 0 is empty, 1-9 is line, 10+ (MinMapValueForObstacle) is wall.
    /// Events indicate sensor status changes, they are not fired continuously, only once.
    /// </summary>
    public class LineAndWallDetectorRobot : RobotBase
    {
        public LineSensor LineSensor { get; set; }
        public FixedDistanceSensor LeftWallSensor { get; private set; }
        public FixedDistanceSensor RightWallSensor { get; private set; }
        public const int WallSensorMaxDistance = 20;
        public const int MinMapValueForObstacle = 10;

        public LineAndWallDetectorRobot(IEnvironment env) : base(env)
        {
            LineSensor = new LineSensor(this);
            LeftWallSensor = new FixedDistanceSensor(this, -90.0, MinMapValueForObstacle, WallSensorMaxDistance);
            RightWallSensor = new FixedDistanceSensor(this, 90.0, MinMapValueForObstacle, WallSensorMaxDistance);
        }

        public override bool CheckAndMoveRobot()
        {
            Point newLocation = Location + Helpers.GetVector(Orientation, Speed);
            if (this.Environment.GetMapValueAtLocation(newLocation) < MinMapValueForObstacle)
            {
                return base.CheckAndMoveRobot();
            }
            else
            {
                Speed = 0.0;
                return false;
            }
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
            double leftDistance = LeftWallSensor.GetDistance();
            UpdateSensorStatus(leftDistance < 20,
                ref lastLeftWallStatus, OnWallOnLeft, OnNoWallOnLeft);
        }

        private bool? lastRightWallStatus = null;
        private void PollRightWallSensor()
        {
            double rightDistance = RightWallSensor.GetDistance();
            UpdateSensorStatus(rightDistance < 20,
                ref lastRightWallStatus, OnWallOnRight, OnNoWallOnRight);
        }
        #endregion

        #region Events indicating sensed environment changes
        public event SensorStatusChangeDelegate OnLineAppears;
        public event SensorStatusChangeDelegate OnLineDisappears;
        public event SensorStatusChangeDelegate OnWallOnLeft;
        public event SensorStatusChangeDelegate OnNoWallOnLeft;
        public event SensorStatusChangeDelegate OnWallOnRight;
        public event SensorStatusChangeDelegate OnNoWallOnRight;

        public delegate void SensorStatusChangeDelegate();
        #endregion
    }
}
