using Environment;
using System.Linq;
using Xunit;

namespace EnvironmentTests
{
    public class MapLowLevelAccess
    {
        private readonly Map map = new Map(100, 100);

        public MapLowLevelAccess()
        {
            // Vertical strips with same (x) values...
            map.Setup((x, y) => x);
        }

        [Fact]
        public void SetupWithLambda()
        {
            for (int i = 0; i < map.SizeX; i++)
                Assert.Equal(i, map[i, 50]);
        }

        [Fact]
        public void ScanLineHorizontal()
        {
            int[] scan = map.GetValuesAlongLine(10, 10, 20, 10).ToArray();
            Assert.Equal(11, scan.Length);
            for (int i = 0; i < 11; i++)
                Assert.Equal(i + 10, scan[i]);
        }

        [Fact]
        public void ScanLineVertical()
        {
            int[] scan = map.GetValuesAlongLine(10, 10, 10, 20).ToArray();
            Assert.Equal(11, scan.Length);
            for (int i = 0; i < 11; i++)
                Assert.Equal(10, scan[i]);
        }

        [Fact]
        public void ScanLineDiagonal()
        {
            int[] scan = map.GetValuesAlongLine(10, 10, 20, 20, 11).ToArray();
            Assert.Equal(11, scan.Length);
            for (int i = 0; i < 11; i++)
                Assert.Equal(i + 10, scan[i]);
        }

        [Fact]
        public void ScanLineGeneric()
        {
            int[] scan = map.GetValuesAlongLine(10, 10, 20, 30).ToArray();
            // Should have correct length and go along all values between 10 and 20 ascending.
            Assert.Equal(23, scan.Length);
            var unqiueValues = scan.Distinct().ToArray();
            Assert.Equal(11, unqiueValues.Length);
            for (int i = 0; i < 11; i++)
                Assert.Equal(10 + i, unqiueValues[i]);
        }

        [Fact]
        public void SetRect()
        {
            map.Setup((x, y) => 0);
            map.SetRect(10, 10, 20, 20, 1);
            Assert.Equal(0, map[9, 10]);
            Assert.Equal(1, map[10, 10]);
            Assert.Equal(1, map[20, 20]);
            Assert.Equal(0, map[21, 20]);
        }
    }
}
