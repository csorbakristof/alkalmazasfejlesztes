using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class TurnState : StateBase
    {
        readonly double targetDirection;
        readonly double angularVelocity;

        public TurnState(double targetDirection, double angularVelocity)
        {
            this.targetDirection = targetDirection;
            this.angularVelocity = angularVelocity;
        }

        public override void Enter()
        {
            Brain.Robot.Turn = angularVelocity;
        }

        public override void Tick()
        {
            if ((angularVelocity > 0 && Brain.Robot.Orientation >= targetDirection)
                || (angularVelocity < 0 && Brain.Robot.Orientation <= targetDirection))
            {
                Brain.CurrentState = new IdleState();
            }
        }
    }
}
