using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public interface IState
    {
        // Brain set by the brain when transitioning into this state.
        //  (Constructor cannot use its value yet.)
        IBrain Brain { get; set; }

        void Enter();

        // Called by Brain when it is notified by the Simulator about a new time tick.
        void Tick();

        void OnLineAppears();
        void OnLineDisappears();
        void OnWallOnLeft();
        void OnNoWallOnLeft();
        void OnWallOnRight();
        void OnNoWallOnRight();

        void OnBeaconClose(int id);
    }
}
