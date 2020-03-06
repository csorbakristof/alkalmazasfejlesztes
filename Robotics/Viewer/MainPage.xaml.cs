using Environment;
using Robot;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Viewer.ViewModel;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.State;

namespace Viewer
{
    public sealed partial class MainPage : Page
    {
        public MapViewModel MapViewModel;
        public RobotViewModel RobotViewModel;

        public DispatcherTimer SimulationTickTimer;

        public MainPage()
        {
            this.InitializeComponent();
            var map = TestMap1Factory.Create();
            this.MapViewModel = new MapViewModel(map);

            var robot = new LineAndWallDetectorRobot(new DefaultEnvironment(map));
            var brain = new WallsAndLinesDemoBrain(robot);
            TestMap1Factory.PutRobotInA(robot);
            brain.AddCommand(new GenericSingleStateCommand(new FollowingLineState()));

            this.RobotViewModel = new RobotViewModel(robot);

            MapImage.Source = this.MapViewModel.Image;

            SimulationTickTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            SimulationTickTimer.Tick += SimulationTickTimer_Tick;
            SimulationTickTimer.Start();
        }

        private void SimulationTickTimer_Tick(object sender, object e)
        {
            RobotViewModel.TriggerSimulationTick();
        }
    }
}
