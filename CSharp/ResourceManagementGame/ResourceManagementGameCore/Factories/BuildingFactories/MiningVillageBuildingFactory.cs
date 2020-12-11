using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.BuildingFactories
{
    public class MiningVillageBuildingFactory :IBuildingFactory
    {
        public Building CreateBuilding()
        {
            return new MiningVillage(ConfigurationManager.GetInstance().GetWorkplaceBuildingMaxCapacity(typeof(MiningVillage)),
                ConfigurationManager.GetInstance().GetBuildingBuildTime(typeof(MiningVillage)));
        }
    }
    public class MiningVillageBuildingRegister : IBuildingRegister
    {
        public IBuildingFactory CreateFactory()
        {
            return new MiningVillageBuildingFactory();
        }

        public Type GetBuildingType()
        {
            return typeof(MiningVillage);
        }
    }
}
