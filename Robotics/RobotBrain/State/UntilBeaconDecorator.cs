using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public class UntilBeaconDecorator : IState
    {
        private readonly IState decorated;
        public IState Follower { get; set; }
        private readonly int beaconId;

        public UntilBeaconDecorator(IState decorated, int beaconId, IState follower)
        {
            this.decorated = decorated;
            this.Follower = follower;
            this.beaconId = beaconId;
        }

        // Note: according to Decorator design pattern, we forward everything to the
        //  decorated IState. The added functionality is only in Tick().
        public IBrain Brain { get => decorated.Brain; set => decorated.Brain = value; }

        public void OnBeaconClose(int id)
        {
            decorated.OnBeaconClose(id);
            if (id == beaconId)
                Brain.CurrentState = Follower;
        }

        public void Tick() => decorated.Tick();
        public void Enter() => decorated.Enter();
        public void OnLineAppears() => decorated.OnLineAppears();
        public void OnLineDisappears() => decorated.OnLineDisappears();
        public void OnNoWallOnLeft() => decorated.OnNoWallOnLeft();
        public void OnNoWallOnRight() => decorated.OnNoWallOnRight();
        public void OnWallOnLeft() => decorated.OnWallOnLeft();
        public void OnWallOnRight() => decorated.OnWallOnRight();

        public override string ToString()
        {
            return $"{nameof(UntilBeaconDecorator)}({decorated})";
        }
    }
}
