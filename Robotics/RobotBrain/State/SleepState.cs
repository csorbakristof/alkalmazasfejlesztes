using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    class SleepState : IState
    {
        int remainingDuration = 0;
        public IBrain Brain { get; set; }

        public SleepState(int duration)
        {
            remainingDuration = duration;
        }

        public void Tick()
        {
            remainingDuration--;
            if (remainingDuration <= 0)
                Brain.CurrentState = new IdleState();
        }
    }
}
