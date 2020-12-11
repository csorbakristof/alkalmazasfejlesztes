using Environment;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Viewer.Helpers
{
    class MapLoader
    {
        public async Task<Map> LoadMap()
        {
            WriteableBitmap bmp = await BitmapFactory.FromContent(
                new Uri("ms-appx:///Assets/MapWithBeacons.png"));
            Map map = new Map(bmp.PixelWidth, bmp.PixelHeight);
            
            Point[] beaconLocations = new Point[4];

            using (bmp.GetBitmapContext())
            {
                for (int y = 0; y < bmp.PixelHeight; y++)
                {
                    for (int x = 0; x < bmp.PixelWidth; x++)
                    {
                        var currentPixel = bmp.GetPixel(x, y);
                        map[x, y] = 0;
                        if (IsObstacleColor(currentPixel))
                        {
                            map[x, y] = 255;
                        }
                        else if (IsLineColor(currentPixel))
                        {
                            map[x, y] = 1;
                        }
                        else
                        {
                            var beaconID = GetBeaconIdOrZero(currentPixel);
                            if (beaconID != 0)
                                beaconLocations[beaconID] = new Point(x, y);
                        }
                    }
                }
            }

            for(int i=1; i<=3; i++)
                map.AddBeacon((int)beaconLocations[i].X,
                    (int)beaconLocations[i].Y,
                    i);
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

        private int GetBeaconIdOrZero(Color c)
        {
            if (c.R < 50 && c.G > 200 && c.B > 200) // Teal
                return 1;
            else if (c.R < 50 && c.G > 200 && c.B < 50) // Green
                return 2;
            else if (c.R > 200 && c.G > 200 && c.B < 50)    // Yellow
                return 3;
            return 0;
        }
    }
}
