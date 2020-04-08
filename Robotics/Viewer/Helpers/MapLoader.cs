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
            WriteableBitmap bmp = await BitmapFactory.FromContent(new Uri("ms-appx:///Assets/Map2.png"));
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
