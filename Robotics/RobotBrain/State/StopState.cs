namespace RobotBrain.State
{
    public class StopState : StateBase
    {
        public override void Tick()
        {
            base.Tick();

            if (Brain.Robot.Speed > 1.0)
                Brain.Robot.Acceleration = -1.0;
            else
            {
                Brain.Robot.Speed = 0.0;
                Brain.Robot.Acceleration = 0.0;
                Brain.CurrentState = new IdleState();
            }
        }
    }
}
