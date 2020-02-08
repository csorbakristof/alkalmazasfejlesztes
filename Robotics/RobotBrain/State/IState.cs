using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public interface IState
    {
        // Brain set by the brain when transitioning into this state.
        IBrain Brain { get; set; }

        // Called by Brain when it is notified by the Simulator about a new time tick.
        void Tick();
    }
}
