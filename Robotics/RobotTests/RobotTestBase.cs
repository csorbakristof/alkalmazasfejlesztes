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
        protected LineSensor lineSensor;
        protected IRobot robot;
        protected DistanceSensor distanceSensor;

        public RobotTestBase()
        {
            map = new Map(100, 100);
            env = new DefaultEnvironment(map);
            var defaultRobot = new RobotBase(env);
            lineSensor = new LineSensor(defaultRobot);
            robot = defaultRobot as IRobot;
            distanceSensor = new DistanceSensor(defaultRobot);

            robot.Location = new Point(50.0, 50.0);
            robot.Orientation = 0.0;
        }
    }
}
