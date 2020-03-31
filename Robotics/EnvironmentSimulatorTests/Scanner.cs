using Environment;
using System;
using System.Linq;
using Xunit;

namespace EnvironmentTests
{
    public class Scanner
    {

        private readonly Map map;
        private readonly IEnvironment env;

        public Scanner()
        {
            map = new Map(100, 100);
            map.SetRect(10, 10, 20, 20, 1);
            env = new DefaultEnvironment(map);
        }

        [Fact]
        public void Instantiate()
        {
            map.SetRect(10, 10, 20, 20, 1);
            var scan = env.Scan(new Point(15, 9), new Point(21, 15)).ToArray();
            Assert.Equal(9, scan.Length);
            Assert.Equal(0, scan[0]);
            Assert.Equal(1, scan[2]);
            Assert.Equal(1, scan[7]);
            Assert.Equal(0, scan[8]);
        }

        [Fact]
        public void RelativePositionCalculation()
        {
            var baseLocOri = new LocOri(0, 0, 0);
            Point pLeft = env.GetLocationOfRelativePoint(baseLocOri, -30.0, 10);
            Assert.Equal(-0.5 * 10.0, pLeft.X);
            Assert.Equal(Math.Round(-Math.Sqrt(3)/ 2.0 * 10.0), pLeft.Y);
            Point pRight = env.GetLocationOfRelativePoint(baseLocOri, 30.0, 10);
            Assert.Equal(0.5 * 10.0, pRight.X);
            Assert.Equal(Math.Round(-Math.Sqrt(3) / 2.0 * 10.0), pRight.Y);
        }

        [Fact]
        public void RelativePositionedScan()
        {
            map.SetRect(9, 0, 11, 20, 1); // Vertical strip
            var scan = env.ScanRelative(new LocOri(10, 15, 0), -30.0, 10, +30.0, 10).ToArray();
            Assert.Equal(11, scan.Length);
            for (int i = 0; i <= 3; i++)
                Assert.Equal(0, scan[i]);
            for (int i = 4; i <= 6; i++)
                Assert.Equal(1, scan[i]);
            for (int i = 7; i <= 10; i++)
                Assert.Equal(0, scan[i]);
        }

        [Fact]
        public void RelativePositionedScanWithGivenLength()
        {
            map.SetRect(9, 0, 11, 20, 1); // Vertical strip
            var scanLength = env.ScanRelative(new LocOri(10, 15, 0), -30.0, 10, +30.0, 10, 11).Count();
            Assert.Equal(11, scanLength);

            scanLength = env.ScanRelative(new LocOri(10, 15, 0), -30.0, 10, +30.0, 10, 10).Count();
            Assert.Equal(10, scanLength);

            // With arbitrary orientation and fixed length
            scanLength = env.ScanRelative(new LocOri(10, 15, 17), -30.0, 10, +30.0, 10, 10).Count();
            Assert.Equal(10, scanLength);
            scanLength = env.ScanRelative(new LocOri(10, 15, 166), -30.0, 10, +30.0, 10, 22).Count();
            Assert.Equal(22, scanLength);
        }

        [Fact]
        public void ScanOutOfMap()
        {
            var scan = env.Scan(new Point(-10,50), new Point(10,50)).ToArray();
            Assert.Equal(21, scan.Length);
            Assert.Equal(0, scan[0]);   // Out-of-range values should be 0.
        }

    }
}
