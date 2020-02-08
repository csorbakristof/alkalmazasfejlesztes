namespace RobotBrain.LogEntry
{
    public class GenericLogEntry : ILogEntry
    {
        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
