using System;
using System.Collections.Generic;

namespace EnvironmentSimulator
{
    public interface IEnvironment
    {
        double Speed { get; set; }
        double Direction { get; set; }
        double Turn { get; set; }

        event OnSpeedChangedDelegate OnSpeedChanged;
        event OnDirectionChangedDelegate OnDirectionChanged;
        event OnTickDelegate OnTick;

        void Tick();

        /// <summary>
        /// Returns the values of the map along the scanline (x1,y1)-(x2,y2)
        /// </summary>
        IEnumerable<int> Scan(int x1, int y1, int x2, int y2);
    }

    public delegate void OnSpeedChangedDelegate(double newValue);
    public delegate void OnDirectionChangedDelegate(double newValue);
    public delegate void OnTickDelegate();
}
