# Things to observe and learn

## C# language elements

- Extension method RobotBrain.State.ExtensionMethods.Then()
	wrapping the use of the decorator design pattern AlterIdleStateDecorator.

## Design patterns

- State pattern: RobotBrain.State namespace
- Decorator pattern: RobotBrain.State.AlterIdleStateDecorator decorating any state with a follower (assuming
	that the original state transitions to IdleState after its task is achieved.)
- Observer: RobotBrain.DefaultBrain is an observer of IEnviroment. DefaultBrain also allows observers via
	the logging mechanism of namespace RobotBrain.LogEntry.

- Visitor design pattern in RobotBrain.LogEntry.ILogEntry, RobotBrain.LogEntry.ILogEntryVisitor,
	see LogAnalysisTests.Instantiation()

