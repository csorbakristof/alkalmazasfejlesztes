using ResourceManagementGameCore.Buildings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories
{
    public static class BuildingFactory
    {
        private static Dictionary<Type, IBuildingFactory> Factories { get; set; } = new Dictionary<Type, IBuildingFactory>();
        public static void AddFactory(IBuildingRegister register)
        {
            Type unitType = register.GetBuildingType();
            IBuildingFactory f = register.CreateFactory();

            if (Factories.ContainsKey(unitType))
                return; //már fel van véve...
            Factories.Add(unitType, f);
        }
        public static Building CreateBuilding(Type unitType)
        {
            IBuildingFactory factory;
            if (Factories.TryGetValue(unitType, out factory))
            {
                return factory.CreateBuilding();
            }
            throw new Exception("Invalid unit type");
        }
        public static void CleanFactories()
        {
            Factories = new Dictionary<Type, IBuildingFactory>();
        }
    }
    public interface IBuildingRegister
    {
        Type GetBuildingType();
        IBuildingFactory CreateFactory();
    }
    public interface IBuildingFactory
    {
        public Building CreateBuilding();
    }
}
