using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public interface ILineSensor
    {
        int[] Scan();
    }
}
