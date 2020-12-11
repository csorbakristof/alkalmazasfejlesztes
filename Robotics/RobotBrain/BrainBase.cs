using Robot;
using RobotBrain.LogEntry;
using RobotBrain.State;

namespace RobotBrain
{
    public class BrainBase : IBrain
    {
        private IState currentState = new IdleState();
        public IState CurrentState 
        {
            get => currentState;
            set
            {
                currentState = value;
                currentState.Brain = this;
                Log(new StateChangeLogEntry(currentState));
                currentState.Enter();
            }
        }

        public IRobot Robot { get; private set; }

        public BrainBase(IRobot robot)
        {
            Robot = robot;
            robot.OnTick += Robot_OnTick;
        }

        #region Handle simulator events
        protected virtual void Robot_OnTick()
        {
            // Note: currentState is changing often. An OnTick event would need
            //  many subscriptions and unsubscriptions, so we use this call
            //  forwarding instead of event.
            currentState.Tick();
            Log(new TickLogEntry());
        }
        #endregion

        protected void Log(ILogEntry entry)
        {
            OnLoggedEvent?.Invoke(entry);
        }

        public event OnLoggedEventDelegate OnLoggedEvent;
    }
}
