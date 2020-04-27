using System.Timers;

namespace Environment
{
    /// <summary>
    /// A timer which can trigger the Tick of an IEnvironment instance.
    /// </summary>
    public class EnvironmentTickSource
    {
        private Timer timer;
        private readonly IEnvironment env;

        public EnvironmentTickSource(IEnvironment env, int timerIntervalMs)
        {
            timer = new Timer(timerIntervalMs);
            timer.Elapsed += Timer_Elapsed;
            this.env = env;
        }

        public void Start()
        {
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            env.Tick();
        }
    }
}

