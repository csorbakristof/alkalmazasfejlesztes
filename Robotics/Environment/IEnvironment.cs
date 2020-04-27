using System;
using System.Collections.Generic;

namespace Environment
{
    public interface IEnvironment
    {
        /// <summary>
        /// Indicates a new tick of time.
        /// </summary>
        event OnTickDelegate OnTick;

        /// <summary>
        /// Calls manually force a time tick.
        /// </summary>
        void Tick();

        Point GetLocationOfRelativePoint(LocOri basePoint, double relativeDirection, double distance);

        /// <summary>
        /// Returns the values of the map along the scanline (x1,y1)-(x2,y2)
        /// </summary>
        IEnumerable<int> Scan(Point p1, Point p2, int? pointNumber = null);

        IEnumerable<int> ScanRelative(LocOri basePoint, double relativeDirection1, double distance1,
            double relativeDirection2, double distance2, int? pointNumber=null);

        int GetMapValueAtLocation(Point p);
    }

    public delegate void OnTickDelegate();
}
