using Environment;
using System;

namespace Robot
{
    public class DefaultRobot : IRobot
    {
        private LocOri LocationOrientation;

        public Point Position
        {
            get => LocationOrientation.Location;
            set => LocationOrientation.Location = value;
        }

        public double Direction
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
    }
}
