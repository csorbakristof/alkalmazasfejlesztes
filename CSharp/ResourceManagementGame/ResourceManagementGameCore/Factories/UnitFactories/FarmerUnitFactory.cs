using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.UnitFactories
{
    public class FarmerUnitFactory : IUnitFactory
    {
        private Farmer newUnit;
        public Unit CreateUnit()
        {
            newUnit = new Farmer(ConfigurationManager.GetInstance().GetUnitFoodConsume(typeof(Farmer)),
                ConfigurationManager.GetInstance().GetAllMappingForConverterUnit(typeof(Farmer)),
                ConfigurationManager.GetInstance().GetConverterUnitMaxInput(typeof(Farmer)));
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
    public class FarmerUnitRegister : IUnitRegister
    {
        public IUnitFactory CreateFactory()
        {
            return new FarmerUnitFactory();
        }

        public Type GetUnitType()
        {
            return typeof(Farmer);
        }
    }
}
