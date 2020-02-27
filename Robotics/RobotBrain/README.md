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

- RobotBrain.State.IState cannot be very generic anymore, as robots events need to be enumerated
	(if we strictly follow the pattern and do not poll everything in OnTick()).

- Test hierarchy across projects: WallFollowerRobotTests.Basics inherits from RobotTests.RobotTestBase


- WallFollowerRobot.SmartSideSensor.PollSensorAndFireEventIfNeeded: event handler for OnTick, but name
	is not about being an event handler, but about what is does.


- Robot.RobotBase.Environment_OnTick calls own virtual method to allow an insert point for sensor check before
	the OnTick event is fired, so that the OnTick will be the last one to fire.

- Named parameters for improved readability: RobotTests.RobotEventTests.ctor