using System;
using System.Collections.Generic;

namespace EnvironmentSimulator
{
    public interface IEnvironment
    {
        double Speed { get; set; }
        double Direction { get; set; }
        double Turn { get; set; }
        Point Position { get; set; }

        event OnSpeedChangedDelegate OnSpeedChanged;
        event OnDirectionChangedDelegate OnDirectionChanged;
        event OnTickDelegate OnTick;

        void Tick();

        Point GetLocationOfRelativePoint(double relativeDirection, double distance);

        /// <summary>
        /// Returns the values of the map along the scanline (x1,y1)-(x2,y2)
        /// </summary>
        IEnumerable<int> Scan(int x1, int y1, int x2, int y2);

        IEnumerable<int> ScanRelative(double relativeDirection1, double distance1,
            double relativeDirection2, double distance2);
    }

    public delegate void OnSpeedChangedDelegate(double newValue);
    public delegate void OnDirectionChangedDelegate(double newValue);
    public delegate void OnTickDelegate();

    public struct Point
    {
        public int X;
        public int Y;
    }
}
