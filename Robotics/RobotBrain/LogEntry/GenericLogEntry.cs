namespace RobotBrain.LogEntry
{
    public class GenericLogEntry : ILogEntry
    {
        public string Message { get; set; }

        public GenericLogEntry(string message)
        {
            this.Message = message;
        }

        public void Accept(ILogEntryVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
