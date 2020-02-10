using Environment;
using Robot;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;

namespace RobotBrain
{
    public class DefaultBrain : IBrain
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

        public DefaultBrain(IRobot robot)
        {
            Robot = robot;
            robot.OnDirectionChanged += Environment_OnDirectionChanged;
            robot.OnSpeedChanged += Environment_OnSpeedChanged;
            robot.OnTick += Environment_OnTick;
        }

        #region Handle simulator events
        private void Environment_OnSpeedChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new GenericLogEntry());
        }

        private void Environment_OnDirectionChanged(double newValue)
        {
            OnLoggedEvent?.Invoke(new GenericLogEntry());
        }

        private void Environment_OnTick()
        {
            currentState.Tick();
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
