using Environment;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Viewer.ViewModel
{
    public class MapViewModel
    {
        public WriteableBitmap Image;

        public ObservableCollection<BeaconViewModel> Beacons =
            new ObservableCollection<BeaconViewModel>();

        public MapViewModel()
        {
        }

        public void SetMap(Map newMap)
        {
            Image = ImageFromMap(newMap);
            foreach(var b in newMap.Beacons)
            {
                Beacons.Add(new BeaconViewModel(b.X, b.Y, b.Id));
            }
        }

        private WriteableBitmap ImageFromMap(Map map)
        {
            var img = new WriteableBitmap(map.SizeX, map.SizeY);
            var colorDict = new Dictionary<byte, Color>
            {
                { 0, Colors.White },
                { 1, Colors.Blue },
                { 255, Colors.Gray }
            };

            using (img.GetBitmapContext())
            {
                for (int x = 0; x < map.SizeX; x++)
                {
                    for (int y = 0; y < map.SizeY; y++)
                    {
                        byte mapValue = (byte)(map[x, y]);
                        img.SetPixel(x, y, colorDict[mapValue]);
                    }
                }
            }

            return img;
        }
    }
}
