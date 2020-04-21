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
            OnLoggedEvent?.Invoke(new TickLogEntry());

            if (currentCommand?.IsComplete() ?? false)
                OnLoggedEvent?.Invoke(new CommandCompleteLogEntry(currentCommand));
        }
        #endregion

        public void AddCommand(ICommand cmd)
        {
            OnLoggedEvent?.Invoke(new GenericLogEntry($"New command received"));
            currentCommand = cmd;
            currentCommand.Brain = this;
            cmd.Execute();
        }

        public event OnLoggedEventDelegate OnLoggedEvent;
    }
}
