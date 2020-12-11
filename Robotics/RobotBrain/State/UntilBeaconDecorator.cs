namespace RobotBrain.State
{
    public class UntilBeaconDecorator : UntilDecoratorBase
    {
        private readonly int beaconId;

        public UntilBeaconDecorator(IState decorated, int beaconId, IState follower)
            : base(decorated, follower)
        {
            this.beaconId = beaconId;
        }

        public override void OnBeaconClose(int id)
        {
            base.OnBeaconClose(id);
            if (id == beaconId)
                Brain.CurrentState = Follower;
        }
    }
}
