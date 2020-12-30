using Environment;
using Robot;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Viewer.ViewModel;
using RobotBrain;
using RobotBrain.State;
using LogAnalysis;
using Viewer.Helpers;
using System.Collections.ObjectModel;
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

            InitButtonCommands();
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
        private void InitButtonCommands()
        {
            ButtonCommands.Add(new CommandButton("Follow line", Brain, new FollowingLineState(5.0)));
            ButtonCommands.Add(new CommandButton("Follow left wall", Brain, new FollowingWallOnLeftState()));
            ButtonCommands.Add(new CommandButton("Follow right wall", Brain, new FollowingWallOnRightState()));
            ButtonCommands.Add(new CommandButton("Cruise", Brain, new CruiseState()));
            ButtonCommands.Add(new CommandButton("Turn left", Brain, new RotateState(-5.0)));
            ButtonCommands.Add(new CommandButton("Turn right", Brain, new RotateState(5.0)));
            ButtonCommands.Add(new CommandButton("Stop", Brain, new StopState()));

            //AddTask3Button();
            //AddTask4Button();
            //AddTask6Button();
            AddTask10Button();
            //AddBeaconAccelerationTaskButton();
            //AddComplexTaskButton();
        }

        private void AddTask3Button()
        {
            //var state = new FollowingLineWithTimeoutState(5, new StopState(), 100);
            var state = new TimeoutDecorator(
                new FollowingLineState(), 100, new StopState());
            ButtonCommands.Add(new CommandButton("Task3", Brain, state));
        }

        private void AddTask4Button()
        {
            //var state2 = new CruiseWithTimeoutState(5, new StopState(), 50);
            //var state1 = new FollowingLineWithTimeoutState(5, state2, 100);
            var state2 = new TimeoutDecorator(
                new CruiseState(),
                50, new StopState());
            var state1 = new TimeoutDecorator(
                new FollowingLineState(),
                100, state2);
            ButtonCommands.Add(new CommandButton("Task4", Brain, state1));
        }

        private void AddTask6Button()
        {
            //var state = new FollowingLineUntilBeaconState(5, new StopState(), 3);
            var state = new UntilBeaconDecorator(
                new FollowingLineState(),
                3, new StopState());
            ButtonCommands.Add(new CommandButton("Task6", Brain, state));
        }

        private void AddTask10Button()
        {
            var state3 = new TimeoutDecorator(
                new CruiseState(),
                100, new StopState()
                );
            var state2 = new TimeoutDecorator(
                new FollowingWallOnLeftState(),
                50, state3);
            var state1 = new UntilBeaconDecorator(
                new FollowingLineState(),
                3, state2);
            ButtonCommands.Add(new CommandButton("Task10", Brain, state1));
        }

        //private void AddBeaconAccelerationTaskButton()
        //{
        //    const int accelerationBeaconId = 3;
        //    const int decelerationBeaconId = 1;
        //    var followSlowly = new FollowingLineState(4);
        //    var followFaster = new FollowingLineState(6);
        //    var slowState = new UntilBeaconDecorator(
        //        followSlowly, accelerationBeaconId, null);
        //    var fastState = new UntilBeaconDecorator(
        //        followFaster, decelerationBeaconId, slowState);
        //    slowState.Follower = fastState;
        //    ButtonCommands.Add(new CommandButton("Task1", Brain, slowState));
        //}


        public ObservableCollection<CommandButton> ButtonCommands
            = new ObservableCollection<CommandButton>();
        #endregion
    }
}
