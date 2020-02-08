﻿using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RobotBrainTests
{
    public class BasicMotionCommands : BrainTestBase
    {
        [Fact]
        public void TurnCommand()
        {
            Assert.True(brain.CurrentState is IdleState);
            Assert.Equal(0.0, sim.Direction, 1);
            brain.AddCommand(new GenericSingleStateCommand(new TurnState(10.0, 1.0)));
            Assert.Null(lastLogEntry);
            for(int i=0; i<10; i++)
                sim.Tick();
            Assert.True(lastLogEntry is CommandComplete);

        }
    }
}