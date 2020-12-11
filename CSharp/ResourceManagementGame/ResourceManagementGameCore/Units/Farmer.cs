using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public class Farmer : ConverterUnit
    {
        public Farmer(int food, Dictionary<string, List<ResourceAmount>> mapping, int maxInput) : base(food, mapping, maxInput)
        {
        }
    }
}
