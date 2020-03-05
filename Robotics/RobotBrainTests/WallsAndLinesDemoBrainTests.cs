using System;
using System.Collections.Generic;
using System.Text;
using Environment;
using Robot;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.State;
using Xunit;

namespace RobotBrainTests
{
    public class WallsAndLinesDemoBrainTests
    {
        private Map map;
        private DefaultEnvironment environment;
        private LineAndWallDetectorRobot robot;
        private WallsAndLinesDemoBrain brain;

        public WallsAndLinesDemoBrainTests()
        {
            map = CreateMap();
            environment = new DefaultEnvironment(map);
            robot = new LineAndWallDetectorRobot(environment);
            brain = new WallsAndLinesDemoBrain(robot);
        }

        #region Drawing the map
        private Map CreateMap()
        {
            Map map = new Map(250, 250);
            DrawHLine(map, x1: 50, x2: 100, y: 50);
            DrawVLine(map, x: 50, y1: 50, y2: 200);
            DrawHLine(map, x1: 50, x2: 100, y: 200);
            DrawVLine(map, x: 100, y1: 50, y2: 100);
            DrawVLine(map, x: 100, y1: 150, y2: 200);
            DrawHLine(map, x1: 100, x2: 150, y: 150);
            DrawFilledRect(map, 110, 60, 190, 140);
            DrawFilledRect(map, 110, 160, 190, 190);
            return map;
        }

        private void DrawHLine(Map map, int x1, int x2, int y, int value = 1)
        {
            for (int x = x1; x <= x2; x++)
                map[x, y] = value;
        }

        private void DrawVLine(Map map, int x, int y1, int y2, int value = 1)
        {
            for (int y = y1; y <= y2; y++)
                map[x, y] = value;
        }

        private void DrawFilledRect(Map map, int x1, int y1, int x2, int y2, int value = 255)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    map[x, y] = value;
        }
        #endregion

        // To test: proper state transitions upon signals from robot
        [Fact]
        public void InitialState()
        {
            Assert.True(brain.CurrentState is IdleState);
        }

        // Following a straight line
        [Fact]
        public void CommandToEnterFollowingLineState()
        {
            // put robot over the line
            robot.Location = new Point(50, 100);
            robot.Orientation = 0.0;
            brain.AddCommand(new GenericSingleStateCommand(new FollowingLineState()));
            Assert.True(brain.CurrentState is FollowingLineState);

            Assert.Equal(0.0, robot.Acceleration, 2);
            environment.Tick();
            // State should accelerate
            Assert.True(robot.Acceleration > 0.1);
        }

        // Command to start following the wall on the left


        // Command to start following the wall on the right


        //  Separate test classes for the follower states (base class, as following is similar)

    }
}
