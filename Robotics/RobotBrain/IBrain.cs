using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;

namespace RobotBrain
{
    public interface IBrain
    {
        event OnLoggedEventEvent OnLoggedEvent;

        void AddCommand(ICommand cmd);

        IState CurrentState { get; set; }
    }

    public delegate void OnLoggedEventEvent(ILogEntry newEntry);
}
