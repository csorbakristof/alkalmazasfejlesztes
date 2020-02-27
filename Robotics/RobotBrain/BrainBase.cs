using Environment;
using Robot;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;

namespace RobotBrain
{
    public class BrainBase : IBrain
    {
        ICommand currentCommand = null;

        private IState currentState = new IdleState();
        public IState CurrentState 
        {
            get => currentState;
            set
            {
                currentState = value;
                currentState.Brain = this;
                currentState.Enter();
            }
        }

        public IRobot Robot { get; private set; }

        public BrainBase(IRobot robot)
        {
            Robot = robot;
            robot.OnTick += Environment_OnTick;
        }

        #region Handle simulator events
        protected virtual void Environment_OnTick()
        {
            currentState.Tick();
            OnLoggedEvent?.Invoke(new GenericLogEntry());

            if (currentCommand?.IsComplete() ?? false)
                OnLoggedEvent?.Invoke(new CommandCompleteLogEntry());
        }
        #endregion

        public void AddCommand(ICommand cmd)
        {
            currentCommand = cmd;
            currentCommand.Brain = this;
            cmd.Execute();
        }

        public event OnLoggedEventDelegate OnLoggedEvent;
    }
}
