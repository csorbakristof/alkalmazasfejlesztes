using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public class ConstructionUnit :Unit
    {
        public int ProgressAmount { get; protected set; }
        public ConstructionUnit(int food, int progress) :base(food)
        {
            ProgressAmount = progress;
        }
        public int DoProgress()
        {
            switch (this.Satisfaction)
            {
                case (Satisfaction.Happy):
                    return ProgressAmount * 2;
                case (Satisfaction.Ok):
                    return ProgressAmount;
                default:
                    return 0;
            }
        }
    }
}
