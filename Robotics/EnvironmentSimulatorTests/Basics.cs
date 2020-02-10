using Environment;
using Robot;
using Xunit;

namespace EnvironmentTests
{
    public class Basics
    {
        readonly IEnvironment env;
        readonly IRobot robot;

        public Basics()
        {
            env = new DefaultEnvironment(null);
            robot = new DefaultRobot(env);
        }

        [Fact]
        public void SetSpeedDirection_ExpectNotification()
        {
            double reportedSpeed = 0.0;
            robot.OnSpeedChanged += (double newValue) => { reportedSpeed = newValue; };
            robot.Speed = 1.0;
            double reportedDirection = 0.0;
            robot.OnDirectionChanged += (double newValue) => { reportedDirection = newValue; };
            robot.Direction = 1.0;
            Assert.Equal(1.0, reportedDirection);
            Assert.Equal(1.0, robot.Direction, 1);
        }

        [Fact]
        public void Turns()
        {
            double reportedDirection = 0.0;
            robot.OnDirectionChanged += (double newValue) => { reportedDirection = newValue; };
            robot.Direction = 0.0;
            robot.Turn = 1.0;
            env.Tick();
            Assert.Equal(1.0, reportedDirection);
        }
    }
}
