using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class TimeoutStateDecorator : IState
    {
        private readonly IState decorated;
        private readonly IState follower;
        private int timeout;

        public TimeoutStateDecorator(IState decorated, int timeoutInTicks, IState follower)
        {
            this.decorated = decorated;
            this.follower = follower;
            this.timeout = timeoutInTicks;
        }

        // Note: according to Decorator design pattern, we forward everything to the
        //  decorated IState. The added functionality is only in Tick().
        public IBrain Brain { get => decorated.Brain; set => decorated.Brain = value; }

        public void Tick()
        {
            decorated.Tick();
            timeout--;
            if (timeout <= 0)
                Brain.CurrentState = follower;
        }

        public void Enter() => decorated.Enter();
        public void OnLineAppears() => decorated.OnLineAppears();
        public void OnLineDisappears() => decorated.OnLineDisappears();
        public void OnNoWallOnLeft() => decorated.OnNoWallOnLeft();
        public void OnNoWallOnRight() => decorated.OnNoWallOnRight();
        public void OnWallOnLeft() => decorated.OnWallOnLeft();
        public void OnWallOnRight() => decorated.OnWallOnRight();
        public void OnBeaconClose(int id) => decorated.OnBeaconClose(id);
    }
}
