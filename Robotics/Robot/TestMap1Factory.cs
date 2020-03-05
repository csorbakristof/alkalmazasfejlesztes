using Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public class TestMap1Factory
    {
        public static Map Create()
        {
            Map map = new Map(250, 250);
            map.DrawHLine(x1: 50, x2: 100, y: 50);
            map.DrawVLine(x: 50, y1: 50, y2: 200);
            map.DrawHLine(x1: 50, x2: 100, y: 200);
            map.DrawVLine(x: 100, y1: 50, y2: 100);
            map.DrawVLine(x: 100, y1: 150, y2: 200);
            map.DrawHLine(x1: 100, x2: 150, y: 150);
            map.DrawFilledRect(110, 60, 190, 140);
            map.DrawFilledRect(110, 160, 190, 190);
            return map;
        }

        public static void PutRobotInA(IRobot robot)
        {
            robot.Location = new Point(50, 100);
            robot.Orientation = 0.0;
        }

        public static void PutRobotInB(IRobot robot)
        {
            robot.Location = new Point(150, 150);
            robot.Orientation = 270.0;
        }
    }
}
