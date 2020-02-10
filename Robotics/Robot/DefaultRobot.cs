using Environment;
using System;

namespace Robot
{
    public class DefaultRobot : IRobot
    {
        double speed;
        public double Speed
        {
            get => speed;
            set
            {
                if (!Double.Equals(speed, value))
                {
                    speed = value;
                    OnSpeedChanged?.Invoke(speed);
                }
            }
        }

        double direction;
        public double Direction
        {
            get => direction;
            set
            {
                if (!Double.Equals(direction, value))
                {
                    direction = value;
                    OnDirectionChanged?.Invoke(direction);
                }
            }
        }

        public IEnvironment Environment { get; set; }

        public DefaultRobot(IEnvironment env)
        {
            Environment = env;
            env.OnTick += Env_OnTick;
        }

        private void Env_OnTick()
        {
            // TODO use LogOri for abs and speed and acceleration
            Direction += Turn;
            OnTick?.Invoke();
        }

        public double Turn { get; set; }
        public Point Position { get; set; }

        public event OnSpeedChangedDelegate OnSpeedChanged;
        public event OnDirectionChangedDelegate OnDirectionChanged;
        public event OnTickDelegate OnTick;
    }
}
