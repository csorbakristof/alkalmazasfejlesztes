using System;
using System.Collections.Generic;

namespace EnvironmentSimulator
{
    public interface ISimulator
    {
        double Speed { get; set; }
        double Direction { get; set; }
        double Turn { get; set; }

        event OnSpeedChangedEvent OnSpeedChanged;
        event OnDirectionChangedEvent OnDirectionChanged;

        void Tick();
    }

    public delegate void OnSpeedChangedEvent(double newValue);
    public delegate void OnDirectionChangedEvent(double newValue);
}
