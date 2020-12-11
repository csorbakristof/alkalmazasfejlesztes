using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public abstract class ProducerUnit :Unit
    {
        public List<ResourceAmount> Products { get; private set; }
        public ProducerUnit(int food, List<ResourceAmount> products) :base(food)
        {
            Products = products.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
        }

        public virtual List<ResourceAmount> Produce()
        {
            switch (this.Satisfaction)
            {
                case (Satisfaction.Unsatisfied):
                    return new List<ResourceAmount>();
                case (Satisfaction.Ok):
                    return Products.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
                case (Satisfaction.Happy):
                    return Products.Select(r => new ResourceAmount(r.Type, r.Amount*2)).ToList();
            }
            return new List<ResourceAmount>();
        }
    }
}
