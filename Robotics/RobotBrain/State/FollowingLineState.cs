using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class FollowingLineState : StateBase
    {
        public override void OnLineAppears()
        {
            base.OnLineAppears();
            if (this.Brain.Robot.Speed < 1.0)
                this.Brain.Robot.Acceleration = 1.0;
            else
                this.Brain.Robot.Acceleration = 0.0;
        }

    }
}
