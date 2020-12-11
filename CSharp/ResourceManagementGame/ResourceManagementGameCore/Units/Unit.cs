using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public abstract class Unit
    {
        public Satisfaction Satisfaction { get; private set; }
        public int FoodNeeded { get; protected set; }
        public Unit(int food)
        {
            FoodNeeded = food;
            Satisfaction = Satisfaction.Ok;
        }
        public void NotifySatisfactionChange(Satisfaction satisfaction)
        {
            Satisfaction = satisfaction;
        }

        public bool ConsumedFood(ResourceAmount resource)
        {
            if (resource == null) return false;
            if (resource.Amount >= FoodNeeded) return false;

            return true;
        }
    }
}
