using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    class IdleState : IState
    {
        public IBrain Brain { get; set; }

        public void Tick() { }
    }
}
