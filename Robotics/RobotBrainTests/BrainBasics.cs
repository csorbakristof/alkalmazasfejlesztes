using EnvironmentSimulator;
using RobotBrain;
using System;
using Xunit;

namespace RobotBrainTests
{
    public class BrainBasics
    {
        [Fact]
        public void Instantiation()
        {
            ISimulator sim = new DefaultSimulator();
            IBrain brain = new DefaultBrain(sim);
        }
    }
}
