using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class StateBase : IState
    {
        public IBrain Brain { get; set; }

        public virtual void Enter() { }

        public virtual void Tick() { }
    }
}
