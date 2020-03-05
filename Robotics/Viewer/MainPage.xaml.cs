using Environment;
using Robot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

namespace Viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Map map = TestMap1Factory.Create();
            var bmp = new WriteableBitmap(map.SizeX, map.SizeY);
            var colorDict = new Dictionary<byte, Color>();
            colorDict.Add(0, Colors.White);
            colorDict.Add(1, Colors.Blue);
            colorDict.Add(255, Colors.Gray);

            using (bmp.GetBitmapContext())
            {
                for (int x = 0; x < map.SizeX; x++)
                {
                    for (int y = 0; y < map.SizeY; y++)
                    {
                        byte mapValue = (byte)(map[x, y]);
                        bmp.SetPixel(x, y, colorDict[mapValue]);
                    }
                }
            }

            var robot = new LineAndWallDetectorRobot(new DefaultEnvironment(map));
            TestMap1Factory.PutRobotInA(robot);

            RobotEllipse.SetValue(Canvas.LeftProperty, robot.Location.X * 2 - 5);
            RobotEllipse.SetValue(Canvas.TopProperty, robot.Location.Y * 2 - 5);

            Image.Source = bmp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Render map to WriteableBitmap
            // Draw robot LocOri on bitmap
            // Draw bitmap on MainCanvas

        }
    }
}
