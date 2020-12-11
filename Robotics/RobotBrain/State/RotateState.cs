using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class RotateState : StateBase
    {
        protected readonly double angularVelocity;

        public RotateState(double angularVelocity)
        {
            this.angularVelocity = angularVelocity;
        }

        public override void Enter()
        {
            Brain.Robot.Turn = angularVelocity;
        }

    }
}
