namespace RobotBrain.LogEntry
{
    public interface ILogEntry
    {
        // Visitor design pattern
        void Accept(ILogEntryVisitor visitor);
    }
}