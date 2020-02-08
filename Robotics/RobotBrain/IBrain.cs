using System;

namespace RobotBrain
{
    public interface IBrain
    {
        event OnLoggedEventEvent OnLoggedEvent;
    }

    public delegate void OnLoggedEventEvent(ILogEntry newEntry);
}
