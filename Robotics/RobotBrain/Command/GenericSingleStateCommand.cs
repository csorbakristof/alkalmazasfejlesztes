using RobotBrain.State;

namespace RobotBrain.Command
{
    /// <summary>
    /// Command which enters given state and completes when Brain has moved to other state.
    /// </summary>
    public class GenericSingleStateCommand : ICommand
    {
        public IBrain Brain { get; set; }

        private readonly IState state;

        public GenericSingleStateCommand(IState state)
        {
            this.state = state;
        }

        public void Execute()
        {
            this.Brain.CurrentState = state;
        }

        public bool IsComplete()
        {
            return this.Brain.CurrentState != state;
        }
    }
}
