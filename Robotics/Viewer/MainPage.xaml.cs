using Environment;
using Robot;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Viewer.ViewModel;
using RobotBrain;
using RobotBrain.Command;
using RobotBrain.State;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;
using LogAnalysis;
using Viewer.Helpers;

namespace Viewer
{
    public sealed partial class MainPage : Page
    {
        public MapViewModel MapViewModel;
        public RobotViewModel RobotViewModel;
        public LogViewModel LogViewModel = new LogViewModel();

        private WallsAndLinesDemoBrain Brain;

        public EnvironmentTickSource EnvironmentTickSource;

        private DefaultEnvironment environment;

        private const int SimulationCycleLengthMs = 100;

        public MainPage()
        {
            this.InitializeComponent();
            environment = new DefaultEnvironment(new Map(1,1)); // Did not load the map yet...
            EnvironmentTickSource = new EnvironmentTickSource(environment, SimulationCycleLengthMs);

            var robot = new LineAndWallDetectorRobot(environment, wallSensorMaxDistance:50);
            Brain = new WallsAndLinesDemoBrain(robot);

            var collector = new LogCollector(Brain, this.LogViewModel);

            this.RobotViewModel = new RobotViewModel(robot);
            RobotImage.Source = RobotViewModel.Image;

            this.MapViewModel = new MapViewModel();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var mapLoader = new MapLoader();
            var map = await mapLoader.LoadMap();
            environment.Map = map;

            RobotViewModel.Robot.Location = new Environment.Point(map.SizeX / 2, map.SizeY / 2);
            RobotViewModel.Robot.Orientation = 0.0;

            MapViewModel.SetMap(map);
            MapImage.Source = MapViewModel.Image;

            // Before simulation starts, update UI with initial values.
            RobotViewModel.NotifyAllPropertyChanges();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        { 
            Brain.AddCommand(new GenericSingleStateCommand(new FollowingLineState(5.0)));
            EnvironmentTickSource.Start();
            RobotViewModel.StartMonitoringModelProperties();
        }
    }
}
