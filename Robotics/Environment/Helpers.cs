using System;

namespace Environment
{
    public class Helpers
    {
        public static double ToRad(double degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

        public static Point GetVector(double orientation, double speed)
        {
            // Orientation 0.0 is upwards, positive direction is clockwise.
            return new Point()
            {
                X = Math.Sin(ToRad(orientation)) * speed,
                Y = -Math.Cos(ToRad(orientation)) * speed
            };
        }
    }
}
