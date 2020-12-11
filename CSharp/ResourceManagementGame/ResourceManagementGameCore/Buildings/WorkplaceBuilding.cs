using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public abstract class WorkplaceBuilding : Building
    {
        public int MaxCapacity { get; protected set; }
        public int CurrentCapacity { get => Units.Count; protected set { } }
        public List<Type> UnitTypes { get; set; }
        protected List<Unit> Units;
        public WorkplaceBuilding(int time) : base(time)
        {
        }
        public virtual void AssignUnit(Unit unit)
        {
            if (UnitTypes.Any(u => u==unit.GetType()))
            {
                if (CurrentCapacity >= MaxCapacity)
                    throw new Exception($"Building is at max capacity {this.GetType().Name}");
                Units.Add(unit);
            }
            else
                throw new Exception($"Invalid unit ({unit.GetType().Name}) for building {this.GetType().Name}");
        }
        public virtual Unit RemoveLastUnit()
        {
            if (Units.Count != 0)
            {
                var last = Units.Last();
                Units.Remove(last);
                return last;
            }
            else
                return null;
        }
        public abstract List<ResourceAmount> DoWork();
    }
}
