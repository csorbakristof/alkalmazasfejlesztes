namespace RobotBrain.Command
{
    public class NopCommand : ICommand
    {
        public IBrain Brain { get; set; }

        public void Execute() { }

        public bool IsComplete() => true;
    }
}
