using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.UnitFactories
{
    public class GathererUnitFactory : IUnitFactory
    {
        private Gatherer newUnit;
        public Unit CreateUnit()
        {
            newUnit = new Gatherer(ConfigurationManager.GetInstance().GetUnitFoodConsume(typeof(Gatherer)),
                ConfigurationManager.GetInstance().GetAllProductsForProducerUnit(typeof(Gatherer)),
                ConfigurationManager.GetInstance().GetInitialProductsForProducerUnit(typeof(Gatherer)));
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
    public class GathererUnitRegister : IUnitRegister
    {
        public IUnitFactory CreateFactory()
        {
            return new GathererUnitFactory();
        }

        public Type GetUnitType()
        {
            return typeof(Gatherer);
        }
    }
}
