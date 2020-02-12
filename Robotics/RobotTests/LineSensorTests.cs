using Environment;
using Robot;
using System;
using Xunit;

namespace RobotTests
{
    public class LineSensorTests : RobotTestBase
    {
        [Fact]
        public void SimpleFrontSensorScan()
        {
            map.Setup((x, y) => x == 50 ? 1 : 0);
            var scan = lineSensor.Scan();
            for(int i=0; i<11; i++)
                Assert.Equal(i==5 ? 1 : 0, scan[i]);
        }
    }
}
