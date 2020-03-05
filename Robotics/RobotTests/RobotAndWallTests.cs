using Environment;
using Robot;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotTests
{
    public class RobotAndWallTests
    {
        private readonly IEnvironment environment;
        private readonly LineAndWallDetectorRobot robot;

        public RobotAndWallTests()
        {
            var map = TestMap1Factory.Create();
            environment = new DefaultEnvironment(map);
            robot = new LineAndWallDetectorRobot(environment);
        }

        [Fact]
        public void CannotGoIntoWall()
        {
            robot.Location = new Point(150, 150);
            robot.Orientation = 0.0;
            robot.Speed = 1;
            // Wall after 10 steps
            for (int i = 0; i < 20; i++)
                environment.Tick();
            Assert.True(robot.Location.Y >= 140);
        }
    }
}
