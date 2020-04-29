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

- LineAndWallDetectorRobot.UpdateSensorStatus: ref value, delegates as parameters, method extracted to helper
	(Can pass events as parametes in the same class.)
	But in WallAndLineDetectorRobotTests, one needs to be tricky.
	AssertSingleFireEventAtLocation uses nameof and prior subscriptions, uses the fact that every test gets separate
		instance, and uses Assert.Single


- RobotEventTests: similar unit tests refactored, original left in comments intentionally

- WallsAndLinesDemoBrainTests: testing the brain without actual robot (state transitions)
	Unit test class implementing IRobot to simulate it for the brain!


Facade: Robot.FixedDistanceSensor

Robot.RobotBase: "public virtual bool CheckAndMoveRobot()" allows derived classes to implement collisions,
	but now, default behaviour is also available.

- Refactoring example: parametrizing two classes and extracting base class.
commit: "Wall following states (before refactor)" and "DistanceBasedTurningStateBase extracted (after refactor)"
(2020-03-05 15:26:08, hash:	7b2960b046e3515ad40a271d896139eeec8107d3)

- Refactor of WallAndLineDetectorRobotTests: parametrizing two methods and extracting common parts, needed
	significant changes in event mocking. Commits "WallAndLineDetectorRobotTests before refactor"
	and "Revert "WallAndLineDetectorRobotTests before refactor""
	(2020-03-05 17:05:38, hash: 45730ca785312d080b40ded659ea16b669f3ff8c)

Decorator pattern: RobotBrain.State.TimeoutStateDecorator
(Unit test: RobotBrainTests.TimeoutStateDecoratorTests)

Adapter?: LogCollector, nothing will depend on this class, but it connects two other ones.

refactor: Command pattern in Viewer: commit 2020-04-21 "Refactor: ICommand pattern instead of event handlers"

Refactor: helper method extracted for event invocation. commit 2020-04-21 "Refactor: helper method introduced for event invocation"
	Would it have been better to include the ILogEntry instatiation as well,
	but hardwire the log entry type this way?

LogViewModel: VM in MVVM and Visitor at the same time.
	- Run on UI thread, no need to await

The IState interface has methods for receiving robot events (transferred by Brain). Why are they not event delegates?
(Because states change all the time and that would need many event subscriptions and unsubscriptions
at every state change.)

