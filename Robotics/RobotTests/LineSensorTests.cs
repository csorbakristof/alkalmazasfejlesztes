using Environment;
using Robot;
using System;
using Xunit;

namespace RobotTests
{
    public class LineSensorTests
    {
        [Fact]
        public void SimpleFrontSensorScan()
        {
            Map map = new Map(100, 100);
            IEnvironment env = new DefaultEnvironment(map);
            var defaultRobot = new DefaultRobot(env);
            ILineSensor sensor = defaultRobot;
            IRobot robot = defaultRobot;

            map.Setup((x, y) => x == 50 ? 1 : 0);

            robot.Position = new Point(50.0, 50.0);
            robot.Orientation = 0.0;

            var scan = sensor.Scan();
            for(int i=0; i<11; i++)
                Assert.Equal(i==5 ? 1 : 0, scan[i]);
        }
    }
}
