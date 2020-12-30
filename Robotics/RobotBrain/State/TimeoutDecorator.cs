using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
//    public class TimeoutDecorator : IState
    public class TimeoutDecorator : UntilDecoratorBase
    {
        //private readonly IState decorated;
        //private readonly IState Follower;
        private int timeout;

        public TimeoutDecorator(IState decorated, int timeoutInTicks, IState follower)
            : base(decorated, follower)
        {
            //this.decorated = decorated;
            //this.Follower = follower;
            this.timeout = timeoutInTicks;
        }

        // Note: according to Decorator design pattern, we forward everything to the
        //  decorated IState. The added functionality is only in Tick().
        //public IBrain Brain { get => decorated.Brain; set => decorated.Brain = value; }

        public override void Tick()
        {
            decorated.Tick();
            timeout--;
            if (timeout <= 0)
                Brain.CurrentState = Follower;
        }

        //public void Enter() => decorated.Enter();
        //public void OnLineAppears() => decorated.OnLineAppears();
        //public void OnLineDisappears() => decorated.OnLineDisappears();
        //public void OnNoWallOnLeft() => decorated.OnNoWallOnLeft();
        //public void OnNoWallOnRight() => decorated.OnNoWallOnRight();
        //public void OnWallOnLeft() => decorated.OnWallOnLeft();
        //public void OnWallOnRight() => decorated.OnWallOnRight();
        //public void OnBeaconClose(int id) => decorated.OnBeaconClose(id);
    }
}
