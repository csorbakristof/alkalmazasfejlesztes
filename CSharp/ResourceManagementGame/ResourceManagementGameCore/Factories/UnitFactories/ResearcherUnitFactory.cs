using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.UnitFactories
{
    public class ResearcherUnitFactory : IUnitFactory
    {
        private Researcher newUnit;
        public Unit CreateUnit()
        {
            newUnit = new Researcher(ConfigurationManager.GetInstance().GetUnitFoodConsume(typeof(Researcher)),
                ConfigurationManager.GetInstance().GetAllProductsForProducerAndConverterUnit(typeof(Researcher)),
                ConfigurationManager.GetInstance().GetInitialProductsForProducerAndConverterUnit(typeof(Researcher)),
                ConfigurationManager.GetInstance().GetAllMappingForProducerAndConverterUnit(typeof(Researcher)),
                ConfigurationManager.GetInstance().GetInitialMappingForProducerAndConverterUnit(typeof(Researcher)),
                ConfigurationManager.GetInstance().GetConverterUnitMaxInput(typeof(Researcher)));
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
    public class ResearcherUnitRegister : IUnitRegister
    {
        public IUnitFactory CreateFactory()
        {
            return new ResearcherUnitFactory();
        }

        public Type GetUnitType()
        {
            return typeof(Researcher);
        }
    }
}
