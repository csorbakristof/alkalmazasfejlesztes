using System;
using System.Numerics;
using Windows.UI.Xaml.Media.Imaging;

namespace Viewer.ViewModel
{
    public class BeaconViewModel
    {
        public double X;
        public double Y;

        public string Id;
        public BitmapImage Image;
        public Vector3 ImageCenterPoint;
        public Vector3 ImageCenterTranslation;
        public Vector3 LabelTranslation;

        public BeaconViewModel(double x, double y, int id)
        {
            this.X = x;
            this.Y = y;
            this.Id = id.ToString();

            this.Image = new BitmapImage
            {
                UriSource = new Uri(@"ms-appx:///Assets/Beacon.png")
            };
            ImageCenterPoint = new Vector3(16.0F, 16.0F, 0.0F);
            ImageCenterTranslation = new Vector3(-16.0F, -16.0F, 0.0F);
            LabelTranslation = new Vector3(16.0F, 16.0F, 0.0F);
        }
    }
}
