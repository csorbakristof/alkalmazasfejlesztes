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
    }

    public delegate void OnSpeedChangedDelegate(double newValue);
    public delegate void OnDirectionChangedDelegate(double newValue);
    public delegate void OnTickDelegate();
}
