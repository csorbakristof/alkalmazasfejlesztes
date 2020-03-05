using Environment;
using System;
using System.Collections.Generic;
using System.Text;
using Robot;
using Xunit;

namespace RobotTests
{
    public class WallAndLineDetectorRobotEventTests
    {
        private readonly IEnvironment environment;
        private readonly LineAndWallDetectorRobot robot;

        public WallAndLineDetectorRobotEventTests()
        {
            var map = new Map(100, 100);
            environment = new DefaultEnvironment(map);
            robot = new LineAndWallDetectorRobot(environment);
            SubscribeToAllRobotEvents();

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
        public void OverLine_Triggers_OnLineAppears()
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

            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), nameof(robot.OnLineAppears));
        }

        [Fact]
        public void WallOnLeft_Triggers_OnWallOnLeft()
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

            AssertSingleFireEventAtLocation(new Point(70.0, 50.0), nameof(robot.OnWallOnLeft));
        }

        #region Helper for event checks
        private void SubscribeToAllRobotEvents()
        {
            robot.OnLineAppears += () => RobotEventFired(nameof(robot.OnLineAppears));
            robot.OnLineDisappears += () => RobotEventFired(nameof(robot.OnLineDisappears));
            robot.OnWallOnLeft += () => RobotEventFired(nameof(robot.OnWallOnLeft));
            robot.OnNoWallOnLeft += () => RobotEventFired(nameof(robot.OnNoWallOnLeft));
            robot.OnWallOnRight += () => RobotEventFired(nameof(robot.OnWallOnRight));
            robot.OnNoWallOnRight += () => RobotEventFired(nameof(robot.OnNoWallOnRight));
        }

        private List<string> events = new List<string>();
        private void RobotEventFired(string eventname)
        {
            events.Add(eventname);
        }

        private void AssertSingleFireEventAtLocation(Point location, string firedEventName)
        {
            robot.Location = location;
            events.Clear();
            environment.Tick();
            Assert.Single(events, firedEventName);
            // Noting should fire again
            environment.Tick();
            Assert.Single(events, firedEventName);
        }
        #endregion

        [Fact]
        public void NotOverLine_Triggers_OnLineDisappears()
        {
            AssertSingleFireEventAtLocation(new Point(70.0, 50.0), nameof(robot.OnLineDisappears));
        }

        [Fact]
        public void NoWallOnLeft_Triggers_OnNoWallOnLeft()
        {
            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), nameof(robot.OnNoWallOnLeft));
        }

        [Fact]
        public void WallOnRight_Triggers_OnWallOnRight()
        {
            AssertSingleFireEventAtLocation(new Point(50.0, 50.0), nameof(robot.OnWallOnRight));
        }

        [Fact]
        public void NoWallOnRight_Triggers_OnNoWallOnRight()
        {
            AssertSingleFireEventAtLocation(new Point(20.0, 50.0), nameof(robot.OnNoWallOnRight));
        }
    }
}
