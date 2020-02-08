using EnvironmentSimulator;
using System;
using Xunit;

namespace EnvironmentSimulatorTests
{
    public class Basics
    {
        readonly IEnvironment sim = new DefaultSimulator();

        [Fact]
        public void SetSpeedDirection_ExpectNotification()
        {
            double reportedSpeed = 0.0;
            sim.OnSpeedChanged += (double newValue) => { reportedSpeed = newValue; };
            sim.Speed = 1.0;
            double reportedDirection = 0.0;
            sim.OnDirectionChanged += (double newValue) => { reportedDirection = newValue; };
            sim.Direction = 1.0;
            Assert.Equal(1.0, reportedDirection);
            Assert.Equal(1.0, sim.Direction, 1);
        }

        [Fact]
        public void Turns()
        {
            double reportedDirection = 0.0;
            sim.OnDirectionChanged += (double newValue) => { reportedDirection = newValue; };
            sim.Direction = 0.0;
            sim.Turn = 1.0;
            sim.Tick();
            Assert.Equal(1.0, reportedDirection);
        }
    }
}
