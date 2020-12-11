namespace RobotBrain.State
{
    public class TurnState : RotateState
    {
        readonly double targetDirection;

        public TurnState(double targetDirection, double angularVelocity)
            : base(angularVelocity)
        {
            this.targetDirection = targetDirection;
        }

        public override void Tick()
        {
            if ((angularVelocity > 0 && Brain.Robot.Orientation >= targetDirection)
                || (angularVelocity < 0 && Brain.Robot.Orientation <= targetDirection))
            {
                Brain.CurrentState = new IdleState();
            }
        }
    }
}
