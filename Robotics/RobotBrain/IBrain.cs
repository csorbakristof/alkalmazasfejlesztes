﻿using EnvironmentSimulator;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;
using System;

namespace RobotBrain
{
    public interface IBrain
    {
        event OnLoggedEventDelegate OnLoggedEvent;

        void AddCommand(ICommand cmd);

        IState CurrentState { get; set; }

        IEnvironment Environment { get; }
    }

    public delegate void OnLoggedEventDelegate(ILogEntry newEntry);
}