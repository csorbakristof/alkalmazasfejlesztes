namespace RobotBrain.LogEntry
{
    public class CommandCompleteLogEntry : ILogEntry
    {
        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
