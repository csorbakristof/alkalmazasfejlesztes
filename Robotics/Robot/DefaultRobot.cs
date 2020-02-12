using Environment;
using System;
using System.Linq;

namespace Robot
{
    public class DefaultRobot : IRobot, ILineSensor
    {
        private LocOri LocationOrientation;

        public Point Position
        {
            get => LocationOrientation.Location;
            set => LocationOrientation.Location = value;
        }

        public double Orientation
        {
            get => LocationOrientation.Orientation;
            set => LocationOrientation.Orientation = value;
        }

        private double speed;
        public double Speed
        {
            get => speed;
            set => speed = value;
        }

        public double Turn { get; set; }

        public double Acceleration { get; set; }

        public IEnvironment Environment { get; set; }

        public DefaultRobot(IEnvironment env)
        {
            Environment = env;
            env.OnTick += Env_OnTick;
        }

        private void Env_OnTick()
        {
            // TODO use LogOri for abs and speed and acceleration
            LocationOrientation.Orientation += Turn;
            LocationOrientation.Location += Helpers.GetVector(LocationOrientation.Orientation, speed);
            Speed += Acceleration;
            OnTick?.Invoke();
        }

        public event OnTickDelegate OnTick;

        #region ILineSensor implementation
        public int[] Scan()
        {
            // Scan in front of the vehicle +/-30 degrees, 11 pixels wide
            return Environment.ScanRelative(this.LocationOrientation, -30.0, 10.0, 30.0, 10.0).ToArray();
        }
        #endregion
    }
}
