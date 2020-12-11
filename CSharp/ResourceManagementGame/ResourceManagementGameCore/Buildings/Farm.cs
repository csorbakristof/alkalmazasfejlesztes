using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public class Farm : ConverterBuilding
    {
        public Farm(int max, int time) :base(time, max, typeof(Farmer))
        {
        }
    }
}
