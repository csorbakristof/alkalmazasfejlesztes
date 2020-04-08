using RobotBrain.Command;

namespace RobotBrain.LogEntry
{
    public class CommandCompleteLogEntry : ILogEntry
    {
        public string CommandType { get; set; }

        public CommandCompleteLogEntry(ICommand command)
        {
            CommandType = command.GetType().Name;
        }

        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
