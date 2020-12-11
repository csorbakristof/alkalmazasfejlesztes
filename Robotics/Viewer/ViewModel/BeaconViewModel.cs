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
        public Vector3 TextBlockCenterPoint;
        public Vector3 TextBlockTranslation;

        public BeaconViewModel(double x, double y, int id)
        {
            this.X = x;
            this.Y = y;
            this.Id = id.ToString();
            TextBlockCenterPoint = new Vector3(8.0F, 8.0F, 0.0F);
            TextBlockTranslation = new Vector3(-8.0F, -8.0F, 0.0F);
        }
    }
}
