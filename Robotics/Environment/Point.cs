namespace Environment
{
    public struct Point
    {
        public double X;
        public double Y;
        public Point(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Point operator+(Point a, Point b)
        {
            return new Point() { X = a.X + b.X, Y = a.Y + b.Y };
        }
    }
}
