using Environment;

namespace Robot
{
    public interface IRobot
    {
        double Speed { get; set; }
        double Direction { get; set; }
        double Turn { get; set; }
        Point Position { get; set; }

        IEnvironment Environment { get; set; }

        event OnTickDelegate OnTick;
    }

    public delegate void OnTickDelegate();

}
