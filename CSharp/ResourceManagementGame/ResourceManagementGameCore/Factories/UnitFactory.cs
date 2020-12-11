using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories
{
    public static class UnitFactory
    {
        private static Dictionary<Type, IUnitFactory> Factories { get; set; } = new Dictionary<Type, IUnitFactory>();
        public static void AddFactory(IUnitRegister visitor)
        {
            Type unitType = visitor.GetUnitType();
            IUnitFactory f = visitor.CreateFactory();

            if (Factories.ContainsKey(unitType))
                return; //már fel van véve...
            Factories.Add(unitType, f);
        }
        public static Unit CreateUnit(Type unitType)
        {
            IUnitFactory factory;
            if (Factories.TryGetValue(unitType, out factory))
            {
                return factory.CreateUnit();
            }
            throw new Exception("Invalid unit type");
        }
        public static void ModifyFutureUnitsWithResearch(Type unitType, ResearchItem research)
        {
            IUnitFactory factory;
            if (Factories.TryGetValue(unitType, out factory))
            {
                factory.AddResearchUpgrade(research);
            }else
                throw new Exception("Invalid unit type");
        }
        public static void CleanFactories()
        {
            Factories = new Dictionary<Type, IUnitFactory>();
        }
    }
    public interface IUnitRegister
    {
        Type GetUnitType();
        IUnitFactory CreateFactory();
    }
    public interface IUnitFactory
    {
        public Unit CreateUnit();
        public void ApplyPreviousResearchUpgrades();
        public void AddResearchUpgrade(ResearchItem research);
    }
}
