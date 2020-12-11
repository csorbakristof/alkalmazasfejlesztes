using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Environment
{
    public class Map
    {
        private readonly int[,] fields;

        public int SizeX { get => fields.GetLength(0); }
        public int SizeY { get => fields.GetLength(1); }

        public Map(int width, int height)
        {
            fields = new int[width, height];
            Setup((x, y) => 0);
        }

        public int this[int x, int y]
        {
            get
            {
                return IsPointInside(x,y) ? fields[x, y] : 0;
            }
            set
            {
                if (IsPointInside(x, y))
                    fields[x, y] = value;
            }
        }

        private bool IsPointInside(int x, int y) => (x >= 0 && x < SizeX && y >= 0 && y < SizeY);

        /// <summary>
        /// Set all map values using a lambda expression taking the coordinates.
        /// </summary>
        /// <param name="lambda"></param>
        public void Setup(Func<int, int, int> lambda)
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    this[i, j] = lambda(i, j);
        }

        public IEnumerable<int> GetValuesAlongLine(int x1, int y1, int x2, int y2, int? numberOfPoints = null)
        {
            if (!numberOfPoints.HasValue)
                numberOfPoints = (int)Math.Round(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2-y1)*(y2-y1))+1.0);

            return GetValuesAlongFixedLengthLine((double)x1, (double)y1, (double)x2, (double)y2, numberOfPoints.Value);
        }

        private IEnumerable<int> GetValuesAlongFixedLengthLine(double x1, double y1, double x2, double y2, int numberOfPoints)
        {
            for(int i=0; i<numberOfPoints; i++)
            {
                double dx = (x2 - x1) / (numberOfPoints - 1);
                double dy = (y2 - y1) / (numberOfPoints - 1);
                double x = x1 + dx * i;
                double y = y1 + dy * i;
                yield return this[(int)Math.Round(x), (int)Math.Round(y)];
            }
        }

        public void SetRect(int x1, int y1, int x2, int y2, int value)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    this[x, y] = value;
        }

        #region Beacon support
        private List<Beacon> beacons = new List<Beacon>();
        public IEnumerable<Beacon> Beacons => beacons;
        public void AddBeacon(int x, int y, int id)
        {
            beacons.Add(new Beacon() { X=x, Y=y, Id=id });
        }

        public IEnumerable<Beacon> FindCloseBeacons(int x, int y, int maxDistance)
        {
            return Beacons.Where(b => isClose(b, x, y, maxDistance));

            throw new NotImplementedException();
        }

        private bool isClose(Beacon b, int x, int y, double maxDistance)
        {
            return (Math.Sqrt((b.X-x)* (b.X - x) + (b.Y - y)* (b.Y - y)) <= maxDistance);
        }

        public struct Beacon
        {
            public int X;
            public int Y;
            public int Id;
        }

        #endregion

        #region Helper methods
        public void DrawHLine(int x1, int x2, int y, int value = 1)
        {
            for (int x = x1; x <= x2; x++)
                this[x, y] = value;
        }

        public void DrawVLine(int x, int y1, int y2, int value = 1)
        {
            for (int y = y1; y <= y2; y++)
                this[x, y] = value;
        }

        public void DrawFilledRect(int x1, int y1, int x2, int y2, int value = 255)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    this[x, y] = value;
        }
        #endregion
    }
}
