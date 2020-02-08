using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class SleepState : StateBase
    {
        int remainingDuration = 0;

        public SleepState(int duration)
        {
            remainingDuration = duration;
        }

        public override void Tick()
        {
            remainingDuration--;
            if (remainingDuration <= 0)
                Brain.CurrentState = new IdleState();
        }
    }
}
