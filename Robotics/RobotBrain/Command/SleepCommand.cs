using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.Command
{
    public class SleepCommand : ICommand
    {
        public IBrain Brain { get; set; }

        readonly private int duration;

        public SleepCommand(int duration = 1)
        {
            this.duration = duration;
        }

        public void Execute()
        {
            Brain.CurrentState = new SleepState(duration);
        }

        public bool IsComplete()
        {
            // Completes when Brain is no longer in SleepState.
            //  This allows for several ways to exit from that state.
            return !(Brain.CurrentState is SleepState);
        }
    }
}
