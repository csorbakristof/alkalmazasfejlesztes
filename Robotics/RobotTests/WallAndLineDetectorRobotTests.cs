using Environment;
using System;
using System.Collections.Generic;
using System.Text;
using Robot;
using Xunit;

namespace RobotTests
{
    public class WallAndLineDetectorRobotTests
    {
        private readonly IEnvironment environment;
        private readonly LineAndWallDetectorRobot robot;

        public WallAndLineDetectorRobotTests()
        {
            var map = new Map(100, 100);
            environment = new DefaultEnvironment(map);
            robot = new LineAndWallDetectorRobot(environment);

            map.DrawVLine(x: 60, y1: 0, y2: 99, value: 255);    // Wall
            map.DrawVLine(x: 20, y1: 0, y2: 99, value: 1);    // Line
            robot.Orientation = 0.0;
        }

        [Fact]
        public void OverLine_Triggers_OnLineAppears()
        {
            // Check for line presence events. They should not be fired twice after each other if the
            //  line is continuous.
            robot.Location = new Point(20.0, 50.0);
            bool lineAppeared = false;
            bool lineDisappeared = false;
            robot.OnLineAppears += (() => lineAppeared = true);
            robot.OnLineDisappears += (() => lineDisappeared = true);
            environment.Tick();
            Assert.True(lineAppeared);
            Assert.False(lineDisappeared);
            // One reported, continuous line is not reported multiple times.
            lineAppeared = false;
            environment.Tick();
            Assert.False(lineAppeared);
        }

        [Fact]
        public void WallOnLeft_Triggers_OnWallOnLeft()
        {
            robot.Location = new Point(70.0, 50.0);
            bool wallAppeared = false;
            bool wallDisappeared = false;
            robot.OnWallOnLeft += (() => wallAppeared = true);
            robot.OnNoWallOnLeft += (() => wallDisappeared = true);
            environment.Tick();
            Assert.True(wallAppeared);
            Assert.False(wallDisappeared);
            wallAppeared = false;
            environment.Tick();
            Assert.False(wallAppeared);
        }
    }
}
