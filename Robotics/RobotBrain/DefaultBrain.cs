using EnvironmentSimulator;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain
{
    public class DefaultBrain : IBrain
    {
        public DefaultBrain(ISimulator simulator)
        {
            simulator.OnDirectionChanged += Simulator_OnDirectionChanged;
            simulator.OnSpeedChanged += Simulator_OnSpeedChanged;
        }

        private void Simulator_OnSpeedChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new DefaultLogEntry());
        }

        private void Simulator_OnDirectionChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new DefaultLogEntry());
        }

        public event OnLoggedEventEvent OnLoggedEvent;
    }

    internal class DefaultLogEntry : ILogEntry
    {
    }
}
