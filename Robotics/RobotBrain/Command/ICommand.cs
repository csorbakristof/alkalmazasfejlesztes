namespace RobotBrain.Command
{
    public interface ICommand
    {
        // Set by the Brain before starting to execute this command.
        IBrain Brain { get; set; }

        void Execute();
        bool IsComplete();
    }
}
