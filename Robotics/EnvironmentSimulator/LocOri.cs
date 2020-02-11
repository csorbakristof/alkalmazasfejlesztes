namespace Environment
{
    public struct LocOri
    {
        public Point Location;
        public double Orientation;

        public LocOri(double x, double y, double orientation) : this()
        {
            this.Location.X = x;
            this.Location.Y = y;
            this.Orientation = orientation;
        }
    }
}
