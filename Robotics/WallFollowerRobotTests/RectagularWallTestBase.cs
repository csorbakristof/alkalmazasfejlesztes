using Environment;
using RobotTests;
using System;
using System.Collections.Generic;
using System.Text;
using WallFollowerRobot;
using Xunit;

namespace WallFollowerRobotTests
{
    public class RectagularWallTestBase : RobotTestBase
    {
        private const int wallHeight = 200;

        public RectagularWallTestBase() : base()
        {
            map.Setup((x, y) => 0);
            // Rectangular obstacle in the middle
            map.SetRect(40, 40, 60, 60, wallHeight);
            // Robot standing beside it
            robot.Position = new Point(30, 50);
            robot.Orientation = 0.0;
        }

        [Fact]
        public void SmartSideSensor()
        {
            var smartSideSensor = new SmartSideSensor(robot, distanceSensor, 90.0, wallHeight, 200);
            double reportedDistance = 0.0;
            smartSideSensor.OnWallDetected += (dist) => reportedDistance = dist;
            robot.Environment.Tick();
            Assert.Equal(10.0, reportedDistance, 1);
        }
    }
}
