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
        public Point Position { get; set; }

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

        public IEnumerable<int> Scan(Point p1, Point p2)
        {
            return map.GetValuesAlongLine(p1.X, p1.Y, p2.X, p2.Y);
        }

        public Point GetLocationOfRelativePoint(double relativeDirection, double distance)
        {
            //double deg = Direction + relativeDirection;
            //double rad = ToRad(deg);
            //double sinRad = Math.Sin(rad);
            //double cosRad = Math.Cos(rad);
            //double dx = sinRad * distance;
            //double dy = -cosRad * distance;

            // Note: direction 0.0 is upwards, X and Y coordinates are increasing rightwards and downwards.
            return new Point()
            {
                X = Position.X + (int)Math.Round(Math.Sin(ToRad(Direction + relativeDirection)) * distance),
                Y = Position.Y - (int)Math.Round(Math.Cos(ToRad(Direction + relativeDirection)) * distance)
            };
        }

        private double ToRad(double degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        public IEnumerable<int> ScanRelative(double relativeDirection1, double distance1,
            double relativeDirection2, double distance2)
        {
            return Scan(
                GetLocationOfRelativePoint(relativeDirection1, distance1),
                GetLocationOfRelativePoint(relativeDirection2, distance2)
                );
        }
    }
}
