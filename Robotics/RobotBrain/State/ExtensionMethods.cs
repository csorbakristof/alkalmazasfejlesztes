namespace RobotBrain.State
{
    public static class ExtensionMethods
    {
        public static IState Then(this IState self, IState follower)
        {
            return new AfterIdleStateDecorator(self, follower);
        }

        public static IState Timeout(this IState self, int timeoutInTicks, IState follower)
        {
            return new TimeoutDecorator(self, timeoutInTicks, follower);
        }
    }
}
