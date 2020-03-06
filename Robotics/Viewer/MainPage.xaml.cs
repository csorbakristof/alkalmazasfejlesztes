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
using Windows.UI;
using System.Threading.Tasks;

namespace Viewer
{
    public sealed partial class MainPage : Page
    {
        public MapViewModel MapViewModel;
        public RobotViewModel RobotViewModel;

        public DispatcherTimer SimulationTickTimer;

        private DefaultEnvironment environment;

        public MainPage()
        {
            this.InitializeComponent();
            environment = new DefaultEnvironment(null); // Did not load the map yet...
            var robot = new LineAndWallDetectorRobot(environment, wallSensorMaxDistance:50);
            var brain = new WallsAndLinesDemoBrain(robot);
            brain.AddCommand(new GenericSingleStateCommand(new FollowingLineState(5.0)));

            this.RobotViewModel = new RobotViewModel(robot);
            RobotImage.Source = RobotViewModel.Image;

            this.MapViewModel = new MapViewModel();

            SimulationTickTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            SimulationTickTimer.Tick += SimulationTickTimer_Tick;
        }

        private void SimulationTickTimer_Tick(object sender, object e)
        {
            RobotViewModel.TriggerSimulationTick();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var map = await LoadMap();
            environment.Map = map;

            RobotViewModel.Robot.Location = new Point(map.SizeX / 2, map.SizeY / 2);
            RobotViewModel.Robot.Orientation = 0.0;

            MapViewModel.SetMap(map);
            MapImage.Source = MapViewModel.Image;
            SimulationTickTimer.Start();
            RobotViewModel.StartMonitoringModelProperties();
        }

        private async Task<Map> LoadMap()
        {
            WriteableBitmap bmp = await BitmapFactory.FromContent(new Uri("ms-appx:///Assets/Map.png"));
            Map map = new Map(bmp.PixelWidth, bmp.PixelHeight);
            using (bmp.GetBitmapContext())
            {
                for (int y = 0; y < bmp.PixelHeight; y++)
                {
                    for (int x = 0; x < bmp.PixelWidth; x++)
                    {
                        var currentPixel = bmp.GetPixel(x, y);
                        if (IsClearColor(currentPixel))
                        {
                            map[x, y] = 0;
                        }
                        else if (IsObstacleColor(currentPixel))
                        {
                            map[x, y] = 255;
                        }
                        else if (IsLineColor(currentPixel))
                        {
                            map[x, y] = 1;
                        }
                    }
                }
            }
            return map;
        }

        private bool IsClearColor(Color c)
        {
            return c.R > 200 && c.G > 200 && c.B > 200;
        }

        private bool IsObstacleColor(Color c)
        {
            return c.R < 50 && c.G < 50 && c.B < 50;
        }

        private bool IsLineColor(Color c)
        {
            return (c.R < 50 && c.G < 50 && c.B > 200) || (c.R > 200 && c.G < 50 && c.B < 50);
        }
    }
}
