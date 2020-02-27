using Environment;
using System;
using System.Collections.Generic;
using System.Text;
using Robot;
using Xunit;

namespace RobotTests
{
    public class RobotEventTests
    {
        private readonly IEnvironment environment;
        private readonly FollowerRobot robot;

        public RobotEventTests()
        {
            var map = new Map(100, 100);
            environment = new DefaultEnvironment(map);
            robot = new FollowerRobot(environment);

            DrawVerticalLineOnMap(map, x: 60, value: 255);   // Wall
            DrawVerticalLineOnMap(map, x: 20, value: 1);     // Line

            robot.Orientation = 0.0;
        }

        private void DrawVerticalLineOnMap(Map map, int x, int value)
        {
            for (int y = 0; y < 100; y++)
                map[x, y] = value;
        }

        [Fact]
        public void Dummy()
        {
            robot.Location = new Point(20.0, 50.0);
            bool lineAppeared = false;
            bool lineDisappeared = false;
            robot.OnLineAppears += (() => lineAppeared = true);
            robot.OnLineDisappears += (() => lineDisappeared = true);
            environment.Tick();
            Assert.True(lineAppeared);
            Assert.False(lineDisappeared);
        }

    }
}
