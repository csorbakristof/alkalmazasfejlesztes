using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Buildings
{
    public abstract class Building
    {
        protected List<ConstructionUnit> BuilderUnits;
        public int CurrentConstructionUnitCount { get => BuilderUnits.Count; }
        public int BuildingTime { get; private set; }
        public bool AbleToFunction { get; private set; }
        public int BuildProgress { get; private set; }
        public Building(int time)
        {
            BuildingTime = time;
            BuildProgress = 0;
            AbleToFunction = false;
            BuilderUnits = new List<ConstructionUnit>();
        }
        public void AddConstructionUnit(ConstructionUnit unit)
        {
            if (BuilderUnits.Contains(unit)) return;
            BuilderUnits.Add(unit);
        }
        public ConstructionUnit RemoveLastConstructionUnit()
        {
            if (BuilderUnits.Count > 0)
            {
                var last = BuilderUnits.Last();           
                BuilderUnits.Remove(last);           
                return last;
            }
            return null;
        }
        public virtual void DoBuildProcess()
        {
            foreach (var item in BuilderUnits)
            {
                if (BuildProgress + item.DoProgress() < BuildingTime)
                    BuildProgress += item.DoProgress();
                else
                {
                    BuildProgress = BuildingTime;
                    break;
                }
            }
            if (BuildProgress == BuildingTime)
                AbleToFunction = true;
        }
    }
}
