# Things to observe and learn

## Operation principles

- TDD tests are is separate projects.
- Environment (robot or physical motion simulation and sensors) are behind IEnvironment.
	It sends events upon position, direction or sensor value changes.
- The robots IBrain uses a FSM and accepts a Command. Commands typically set a State and then wait
	for it to transition to the IdleState.
- Brain sends log events to the LogAnalysis project.


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

- RobotBrain.State.IState and its defaults in StateBase.

- Robot.IRobot, Robot.ILineSensor szétválasztása (Interface Segregation Principle)

- RobotTests.DistanceSensorTests.wallHeight const

- RobotTests.RobotTestBase: initializations moved into common base class.

- Linq get first index: Robot.DefaultRobot.GetDistance uses TakeWhile and Count to do this.
