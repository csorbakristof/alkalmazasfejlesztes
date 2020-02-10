using Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public interface IRobot
    {
        double Speed { get; set; }
        double Direction { get; set; }
        double Turn { get; set; }
        Point Position { get; set; }

        IEnvironment Environment { get; set; }


        event OnSpeedChangedDelegate OnSpeedChanged;
        event OnDirectionChangedDelegate OnDirectionChanged;

        event OnTickDelegate OnTick;

    }

    public delegate void OnSpeedChangedDelegate(double newValue);
    public delegate void OnDirectionChangedDelegate(double newValue);
    public delegate void OnTickDelegate();

}
