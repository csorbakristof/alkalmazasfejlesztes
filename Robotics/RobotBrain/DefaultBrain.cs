using EnvironmentSimulator;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain
{
    public class DefaultBrain : IBrain
    {
        ICommand currentCommand = null;
        private IState currentState = new IdleState();
        public IState CurrentState 
        {
            get => currentState;
            set
            {
                currentState = value;
                currentState.Brain = this;
            }
        }


        public DefaultBrain(ISimulator simulator)
        {
            simulator.OnDirectionChanged += Simulator_OnDirectionChanged;
            simulator.OnSpeedChanged += Simulator_OnSpeedChanged;
            simulator.OnTick += Simulator_OnTick;
        }

        #region Handle simulator events
        private void Simulator_OnSpeedChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new DefaultLogEntry());
        }

        private void Simulator_OnDirectionChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new DefaultLogEntry());
        }

        private void Simulator_OnTick()
        {
            currentState.Tick();
            if (currentCommand?.IsComplete() ?? false)
                OnLoggedEvent?.Invoke(new CommandComplete());
        }
        #endregion

        public void AddCommand(ICommand cmd)
        {
            currentCommand = cmd;
            currentCommand.Brain = this;
            cmd.Execute();
        }

        public event OnLoggedEventEvent OnLoggedEvent;
    }

    internal class DefaultLogEntry : LogEntry.ILogEntry
    {
    }
}
