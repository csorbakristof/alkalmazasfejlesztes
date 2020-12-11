namespace RobotBrain.State
{
    public class StateBase : IState
    {
        public IBrain Brain { get; set; }

        public virtual void Enter() { }
        public virtual void Tick() { }

        public virtual void OnLineAppears() { }
        public virtual void OnLineDisappears() { }
        public virtual void OnNoWallOnLeft() { }
        public virtual void OnNoWallOnRight() { }
        public virtual void OnWallOnLeft() { }
        public virtual void OnWallOnRight() { }
        public virtual void OnBeaconClose(int id) { }

        public override string ToString() => this.GetType().Name;
    }
}
