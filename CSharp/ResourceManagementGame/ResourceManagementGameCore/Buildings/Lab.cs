using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public class Lab : ProducerAndConverterBuilding
    {
        public Lab(int max, int time) : base(time, max, typeof(Researcher))
        {
        }
    }
}
