using Environment;
using Robot;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Viewer.ViewModel;
using RobotBrain;
using RobotBrain.State;
using LogAnalysis;
using Viewer.Helpers;
using System;

namespace Viewer
{
    public sealed partial class MainPage : Page
    {
        public MapViewModel MapViewModel;
        public RobotViewModel RobotViewModel;
        public LogViewModel LogViewModel = new LogViewModel();

        private readonly WallsAndLinesDemoBrain Brain;

        public EnvironmentTickSource EnvironmentTickSource;

        private readonly DefaultEnvironment environment;

        private const int SimulationCycleLengthMs = 100;

        public MainPage()
        {
            this.InitializeComponent();
            environment = new DefaultEnvironment(new Map(1,1)); // Did not load the map yet...
            EnvironmentTickSource = new EnvironmentTickSource(environment, SimulationCycleLengthMs);

            var robot = new LineAndWallDetectorRobot(environment);
            Brain = new WallsAndLinesDemoBrain(robot);

            new LogCollector(Brain, this.LogViewModel); // Ctor performs registrations

            this.RobotViewModel = new RobotViewModel(robot);
            RobotImage.Source = RobotViewModel.Image;

            this.MapViewModel = new MapViewModel();

            FollowLineCommand = new CommandButtonCommand(Brain, new FollowingLineState(5.0));
            FollowLeftWallCommand = new CommandButtonCommand(Brain, new FollowingWallOnLeftState());
            FollowRightWallCommand = new CommandButtonCommand(Brain, new FollowingWallOnRightState());
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

            AddBeaconsToTheCanvas();

            // Before simulation starts, update UI with initial values.
            RobotViewModel.NotifyAllPropertyChanges();
        }

        private void AddBeaconsToTheCanvas()
        {
            foreach(var b in MapViewModel.Beacons)
            {
                var image = new TextBlock()
                {
                    Text = b.Id,
                    CenterPoint = b.TextBlockCenterPoint,
                    Translation = b.TextBlockTranslation
                };
                MapCanvas.Children.Add(image);
                Canvas.SetLeft(image, b.X);
                Canvas.SetTop(image, b.Y);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        { 
            Brain.CurrentState = new FollowingLineState(5.0);
            EnvironmentTickSource.Start();
            RobotViewModel.StartMonitoringModelProperties();
        }

        #region Command buttons (ICommand pattern)
        public CommandButtonCommand FollowLineCommand;
        public CommandButtonCommand FollowLeftWallCommand;
        public CommandButtonCommand FollowRightWallCommand;
        #endregion
    }
}
