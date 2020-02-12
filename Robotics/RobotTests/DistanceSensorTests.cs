using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotTests
{
    public class DistanceSensorTests : RobotTestBase
    {
        private const int wallHeight = 200;

        [Fact]
        public void Basics()
        {
            map.Setup((x, y) => x == 60 ? wallHeight : 0);
            // Scan left, should find vertical wall at X=60 (robot is at (50;50)).
            var distance = distanceSensor.GetDistance(90.0, wallHeight, 20);
            Assert.Equal(10, distance);
        }
    }
}
