using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.Command
{
    public class SquareCommand : ICommand
    {
        public IBrain Brain { get; set; }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool IsComplete()
        {
            throw new NotImplementedException();
        }
    }
}
