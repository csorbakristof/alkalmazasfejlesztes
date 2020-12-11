namespace RobotBrain.LogEntry
{
    // Visitor design pattern
    public interface ILogEntryVisitor
    {
        void Visit(StateChangeLogEntry logEntry);
        void Visit(GenericLogEntry logEntry);
        void Visit(TickLogEntry logEntry);
        void Visit(RobotEventLogEntry logEntry);
    }
}