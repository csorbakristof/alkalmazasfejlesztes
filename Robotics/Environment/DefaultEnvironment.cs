using System;
using System.Collections.Generic;
using System.Linq;

namespace Environment
{
    public class DefaultEnvironment : IEnvironment
    {
        public Map Map;

        public DefaultEnvironment(Map map)
        {
            this.Map = map;
        }

        public event OnTickDelegate OnTick;

        public void Tick()
        {
            OnTick?.Invoke();
        }

        public IEnumerable<int> Scan(Point p1, Point p2, int? numberOfPoints)
        {
            return Map.GetValuesAlongLine(
                (int)Math.Round(p1.X),
                (int)Math.Round(p1.Y),
                (int)Math.Round(p2.X),
                (int)Math.Round(p2.Y), numberOfPoints);
        }

        public Point GetLocationOfRelativePoint(LocOri baseLocOri, double relativeDirection, double distance)
        {
            // Note: direction 0.0 is upwards, X and Y coordinates are increasing rightwards and downwards.
            return new Point()
            {
                X = baseLocOri.Location.X + (int)Math.Round(Math.Sin(Helpers.ToRad(baseLocOri.Orientation + relativeDirection)) * distance),
                Y = baseLocOri.Location.Y - (int)Math.Round(Math.Cos(Helpers.ToRad(baseLocOri.Orientation + relativeDirection)) * distance)
            };
        }


        public IEnumerable<int> ScanRelative(LocOri basePoint,
            double relativeDirection1, double distance1,
            double relativeDirection2, double distance2, int? pointNumber)
        {
            return Scan(
                GetLocationOfRelativePoint(basePoint, relativeDirection1, distance1),
                GetLocationOfRelativePoint(basePoint, relativeDirection2, distance2),
                pointNumber
                );
        }

        public int GetMapValueAtLocation(Point p)
        {
            return this.Map[(int)Math.Round(p.X), (int)Math.Round(p.Y)];
        }

        public IEnumerable<int> GetCloseBeaconIds(double x, double y, double maxDistance)
        {
            return Map.FindCloseBeacons(
                (int)Math.Round(x),
                (int)Math.Round(y),
                (int)Math.Round(maxDistance)).Select(b => b.Id);
        }
    }
}

