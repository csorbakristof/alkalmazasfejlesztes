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
        public void Turns()
        {
            robot.Direction = 0.0;
            robot.Turn = 1.0;
            env.Tick();
            Assert.Equal(1.0, robot.Direction);
        }
    }
}
