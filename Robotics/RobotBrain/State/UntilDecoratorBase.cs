namespace RobotBrain.State
{
    /// <summary>
    /// Decorator base storing a successor state which can be changed to
    ///     after some kind of condition becomes true.
    /// </summary>
    public class UntilDecoratorBase : IState
    {
        private readonly IState decorated;
        public IState Follower { get; set; }

        public UntilDecoratorBase(IState decorated, IState follower)
        {
            this.decorated = decorated;
            this.Follower = follower;
        }

        // Note: according to Decorator design pattern, we forward everything to the
        //  decorated IState. The added functionality is only in Tick().
        public IBrain Brain { get => decorated.Brain; set => decorated.Brain = value; }

        public virtual void Tick() => decorated.Tick();
        public virtual void Enter() => decorated.Enter();
        public virtual void OnLineAppears() => decorated.OnLineAppears();
        public virtual void OnLineDisappears() => decorated.OnLineDisappears();
        public virtual void OnNoWallOnLeft() => decorated.OnNoWallOnLeft();
        public virtual void OnNoWallOnRight() => decorated.OnNoWallOnRight();
        public virtual void OnWallOnLeft() => decorated.OnWallOnLeft();
        public virtual void OnWallOnRight() => decorated.OnWallOnRight();
        public virtual void OnBeaconClose(int id) => decorated.OnBeaconClose(id);

        public override string ToString()
        {
            return $"{this.GetType().Name}({decorated})";
        }

    }
}
