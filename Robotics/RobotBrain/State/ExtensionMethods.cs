using System;
using System.Collections.Generic;
using System.Text;

namespace RobotBrain.State
{
    public static class ExtensionMethods
    {
        public static IState Then(this IState self, IState follower)
        {
            return new AfterIdleStateDecorator(self, follower);
        }
    }
}
