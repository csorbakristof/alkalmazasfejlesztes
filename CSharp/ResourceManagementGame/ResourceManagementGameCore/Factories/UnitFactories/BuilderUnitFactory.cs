using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.UnitFactories
{
    public class BuilderUnitFactory : IUnitFactory
    {
        
        private Builder newUnit;
        public Unit CreateUnit()
        {
            newUnit = new Builder(ConfigurationManager.GetInstance().GetUnitFoodConsume(typeof(Builder)),
                ConfigurationManager.GetInstance().GetConstructionUnitProgressAmount(typeof(Builder)));
            ApplyPreviousResearchUpgrades();
            return newUnit;
        }
        private List<ResearchItem> Researches = new List<ResearchItem>();
        public void ApplyPreviousResearchUpgrades()
        {
            foreach (var item in Researches)
            {
                item.Apply(newUnit);
            }
        }
        public void AddResearchUpgrade(ResearchItem research)
        {
            Researches.Add(research);
        }
    }
    public class BuilderUnitRegister : IUnitRegister
    {
        public IUnitFactory CreateFactory()
        {
            return new BuilderUnitFactory();
        }

        public Type GetUnitType()
        {
            return typeof(Builder);
        }
    }
}
