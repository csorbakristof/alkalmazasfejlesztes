using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace RobotTests
{
    public class ProximitySensorTests : RobotTestBase
    {
        [Fact]
        public void Basics()
        {
            map.AddBeacon(10, 10, 1);
            map.AddBeacon(100, 100, 2);
            robot.Location = new Environment.Point(20, 20);
            var ids = beaconSensor.GetCloseBeaconIds().ToArray();
            Assert.Single(ids);
            Assert.Equal(1, ids[0]);
        }
    }
}
