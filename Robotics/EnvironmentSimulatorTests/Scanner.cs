using EnvironmentSimulator;
using System.Linq;
using Xunit;

namespace EnvironmentSimulatorTests
{
    public class Scanner
    {
        [Fact]
        public void Instantiate()
        {
            Map map = new Map(100, 100);
            map.SetRect(10, 10, 20, 20, 1);
            IEnvironment env = new DefaultSimulator(map);
            var scan = env.Scan(15, 9, 21, 15).ToArray();
            Assert.Equal(7, scan.Length);
            Assert.Equal(0, scan[0]);
            Assert.Equal(1, scan[2]);
            Assert.Equal(1, scan[5]);
            Assert.Equal(0, scan[6]);

        }
    }
}
