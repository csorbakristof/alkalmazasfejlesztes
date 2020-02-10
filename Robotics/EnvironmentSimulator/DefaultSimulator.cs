using System;
using System.Collections.Generic;
using System.Text;

namespace EnvironmentSimulator
{
    public class DefaultSimulator : IEnvironment
    {
        private readonly Map map;

        public DefaultSimulator(Map map)
        {
            this.map = map;
        }

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

        public double Turn { get; set; }

        public event OnSpeedChangedDelegate OnSpeedChanged;
        public event OnDirectionChangedDelegate OnDirectionChanged;
        public event OnTickDelegate OnTick;

        public void Tick()
        {
            Direction += Turn;
            OnTick?.Invoke();
        }

        public IEnumerable<int> Scan(int x1, int y1, int x2, int y2)
        {
            return map.GetValuesAlongLine(x1, y1, x2, y2);
        }
    }
}
