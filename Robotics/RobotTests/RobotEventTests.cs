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
        public void OverLine_TriggersLinePresence()
        {
            // Check for line presence events. They should not be fired twice after each other if the
            //  line is continuous.

            // Note: this commented part is left intentionally to show original or refactored unit test.
            //robot.Location = new Point(20.0, 50.0);
            //bool lineAppeared = false;
            //bool lineDisappeared = false;
            //robot.OnLineAppears += (() => lineAppeared = true);
            //robot.OnLineDisappears += (() => lineDisappeared = true);
            //environment.Tick();
            //Assert.True(lineAppeared);
            //Assert.False(lineDisappeared);
            //// One reported, continuous line is not reported multiple times.
            //lineAppeared = false;
            //environment.Tick();
            //Assert.False(lineAppeared);

            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), true,
                ref robot.OnLineAppears, ref robot.OnLineDisappears);
        }

        [Fact]
        public void WallOnLeft_TriggersLeftWallPresence()
        {
            // Note: this commented part is left intentionally to show original or refactored unit test.
            //robot.Location = new Point(70.0, 50.0);
            //bool wallAppeared = false;
            //bool wallDisappeared = false;
            //robot.OnWallOnLeft += (() => wallAppeared = true);
            //robot.OnNoWallOnLeft += (() => wallDisappeared = true);
            //environment.Tick();
            //Assert.True(wallAppeared);
            //Assert.False(wallDisappeared);
            //wallAppeared = false;
            //environment.Tick();
            //Assert.False(wallAppeared);

            AssertSingleFireEventAtLocation(new Point(70.0, 50.0), true,
                ref robot.OnWallOnLeft, ref robot.OnNoWallOnLeft);
        }

        private void AssertSingleFireEventAtLocation(Point location, bool expectedStatus,
            ref FollowerRobot.SensorStatusChangeDelegate trueEvent,
            ref FollowerRobot.SensorStatusChangeDelegate falseEvent)
        {
            robot.Location = location;
            bool trueEventFired = false;
            bool falseEventFired = false;
            trueEvent += (() => trueEventFired = true);
            falseEvent += (() => falseEventFired = true);
            environment.Tick();
            Assert.Equal(expectedStatus, trueEventFired);
            Assert.Equal(!expectedStatus, falseEventFired);
            // Noting should fire again
            trueEventFired = false;
            falseEventFired = false;
            environment.Tick();
            Assert.False(trueEventFired);
            Assert.False(falseEventFired);
        }

        [Fact]
        public void NotOverLine_Triggers_OnLineDisappears()
        {
            AssertSingleFireEventAtLocation(new Point(70.0, 50.0), false,
                ref robot.OnLineAppears, ref robot.OnLineDisappears);
        }

        [Fact]
        public void NoWallOnLeft_Triggers_OnNoWallOnLeft()
        {
            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), false,
                ref robot.OnWallOnLeft, ref robot.OnNoWallOnLeft);
        }

        [Fact]
        public void WallOnRight_Triggers_OnWallOnRight()
        {
            AssertSingleFireEventAtLocation(new Point(50.0, 50.0), true,
                ref robot.OnWallOnRight, ref robot.OnNoWallOnRight);
        }

        [Fact]
        public void NoWallOnRight_Triggers_OnNoWallOnRight()
        {
            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), false,
                ref robot.OnWallOnRight, ref robot.OnNoWallOnRight);
        }


    }
}
