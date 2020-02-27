using Environment;

namespace Robot
{
    public interface IRobot
    {
        LocOri LocationOrientation { get; set; }
        Point Location { get; set; }
        double Orientation { get; set; }

        double Speed { get; set; }
        double Turn { get; set; }
        double Acceleration { get; set; }

        IEnvironment Environment { get; set; }

        event OnTickDelegate OnTick;
    }

    public delegate void OnTickDelegate();

}
