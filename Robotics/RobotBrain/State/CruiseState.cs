namespace RobotBrain.State
{
    public class CruiseState : StateBase
    {
        private readonly double targetSpeed;

        public CruiseState(double targetSpeed = 5.0)
        {
            this.targetSpeed = targetSpeed;
        }

        public override void Tick()
        {
            base.Tick();

            Brain.Robot.Turn = 0.0;
            if (Brain.Robot.Speed < targetSpeed)
                Brain.Robot.Acceleration = 1.0;
            else
                Brain.Robot.Acceleration = 0.0;
        }
    }
}
