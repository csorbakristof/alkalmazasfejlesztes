using Microsoft.VisualBasic.CompilerServices;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public abstract class ProducerBuilding : WorkplaceBuilding
    {
        public ProducerBuilding(int max, int time, Type singleUnit) : base(time)
        {
            MaxCapacity = max;
            CurrentCapacity = 0;
            Units = new List<Unit>();
            UnitTypes = new List<Type>();
            UnitTypes.Add(singleUnit);
        }
        public ProducerBuilding(int max, int time,List<Type> listOfUnits) : base(time)
        { 
            MaxCapacity = max;
            UnitTypes = new List<Type>(listOfUnits);
            Units = new List<Unit>();
        }
        public override List<ResourceAmount> DoWork()
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (!AbleToFunction) return result;
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            foreach (var item in Units)
            {
                var producerUnit = item as ProducerUnit;
                producerUnit.Produce().ForEach(p =>
                {
                    if (tmp.ContainsKey(p.Type))
                        tmp[p.Type] += p.Amount;
                    else tmp.Add(p.Type, p.Amount);
                });         
            }
            result.AddRange(tmp.Select(p => new ResourceAmount(p.Key, p.Value)));
            return result;
        }
    }
}
