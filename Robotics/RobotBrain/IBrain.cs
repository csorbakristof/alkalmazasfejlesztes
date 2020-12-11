using Robot;
using RobotBrain.LogEntry;
using RobotBrain.State;

namespace RobotBrain
{
    public interface IBrain
    {
        event OnLoggedEventDelegate OnLoggedEvent;

        IState CurrentState { get; set; }

        IRobot Robot { get; }
    }

    public delegate void OnLoggedEventDelegate(ILogEntry newEntry);
}
