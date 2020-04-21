using System;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.State;

namespace Viewer
{
    public sealed partial class MainPage
    {
        #region Command buttons
        public class CommandButtonCommand : System.Windows.Input.ICommand
        {
            private WallsAndLinesDemoBrain brain;
            private IState targetState;

            public CommandButtonCommand(WallsAndLinesDemoBrain brain, IState targetState)
            {
                this.brain = brain;
                this.targetState = targetState;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                brain.AddCommand(new GenericSingleStateCommand(targetState));
            }
        }
        #endregion
    }
}
