using Environment;
using Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotTests
{
    public class RobotTestBase
    {
        protected Map map;
        protected IEnvironment env;
        protected ILineSensor lineSensor;
        protected IRobot robot;
        protected IDistanceSensor distanceSensor;

        public RobotTestBase()
        {
            map = new Map(100, 100);
            env = new DefaultEnvironment(map);
            var defaultRobot = new DefaultRobot(env);
            lineSensor = defaultRobot as ILineSensor;
            robot = defaultRobot as IRobot;
            distanceSensor = defaultRobot as IDistanceSensor;

            robot.Position = new Point(50.0, 50.0);
            robot.Orientation = 0.0;
        }
    }
}
