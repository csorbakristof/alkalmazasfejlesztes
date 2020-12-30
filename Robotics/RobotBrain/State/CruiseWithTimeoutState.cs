namespace RobotBrain.State
{
    [System.Obsolete("Used only temporarily in the lab. Use TimeoutDecorator instead.")]
    public class CruiseWithTimeoutState : StateBase
    {
        private readonly double targetSpeed;
        private readonly IState followerState;
        private int timeout;

        public CruiseWithTimeoutState(double targetSpeed, IState follower, int timeout)
        {
            this.targetSpeed = targetSpeed;
            this.followerState = follower;
            this.timeout = timeout;
        }

        public override void Tick()
        {
            base.Tick();

            // TASK: added timeout functionality
            timeout--;
            if (timeout <= 0)
                this.Brain.CurrentState = followerState;

            Brain.Robot.Turn = 0.0;
            if (Brain.Robot.Speed < targetSpeed)
                Brain.Robot.Acceleration = 1.0;
            else
                Brain.Robot.Acceleration = 0.0;
        }
    }
}
