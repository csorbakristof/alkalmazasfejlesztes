using Environment;

namespace Robot
{
    public interface IRobot
    {
        Point Position { get; set; }
        double Direction { get; set; }
        double Speed { get; set; }
        double Turn { get; set; }
        double Acceleration { get; set; }

        IEnvironment Environment { get; set; }

        event OnTickDelegate OnTick;
    }

    public delegate void OnTickDelegate();

}
