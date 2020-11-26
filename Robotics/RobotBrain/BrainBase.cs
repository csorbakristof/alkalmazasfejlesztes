using Robot;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System.Collections.Generic;

namespace RobotBrain
{
    public class BrainBase : IBrain
    {
        private Queue<ICommand> pendingCommands = new Queue<ICommand>();
        ICommand currentCommand = null;

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
            //  many subscriptions and unsubscriptions.
            currentState.Tick();

            Log(new TickLogEntry());

            if (currentCommand == null ||
                currentCommand.IsComplete())
            {
                if (currentCommand?.IsComplete() ?? false)
                    Log(new CommandCompleteLogEntry(currentCommand));

                if (pendingCommands.Count > 0)
                    startCommand(pendingCommands.Dequeue());
                else
                    currentCommand = null;
            }
        }
        #endregion

        private void startCommand(ICommand cmd)
        {
            currentCommand = cmd;
            currentCommand.Brain = this;
            cmd.Execute();
        }

        public void AddCommand(ICommand cmd)
        {
            Log(new GenericLogEntry($"New command received"));
            if (currentCommand == null)
                startCommand(cmd);
            else
                pendingCommands.Enqueue(cmd);
        }

        protected void Log(ILogEntry entry)
        {
            OnLoggedEvent?.Invoke(entry);
        }

        public event OnLoggedEventDelegate OnLoggedEvent;
    }
}
