using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class UntilLineAppearsDecorator : UntilDecoratorBase
    {
        public UntilLineAppearsDecorator(IState decorated, IState follower)
            : base(decorated, follower)
        {
        }

        public override void OnLineAppears()
        {
            this.Brain.CurrentState = Follower;
        }
    }
}
