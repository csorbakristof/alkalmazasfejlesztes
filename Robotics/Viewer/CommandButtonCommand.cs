using System;
using RobotBrain;
using RobotBrain.State;

namespace Viewer
{
    public sealed partial class MainPage
    {
        #region Command buttons
        public class CommandButtonCommand : System.Windows.Input.ICommand
        {
            private readonly WallsAndLinesDemoBrain brain;
            private readonly IState targetState;

            public CommandButtonCommand(WallsAndLinesDemoBrain brain, IState targetState)
            {
                this.brain = brain;
                this.targetState = targetState;
            }

            public event EventHandler CanExecuteChanged
            {
                // This event never happens as CanExecute is constant.
                add { }
                remove { }
            }

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                brain.CurrentState = targetState;
            }
        }
        #endregion
    }
}
