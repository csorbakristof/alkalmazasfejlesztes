using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.BuildingFactories
{
    public class FarmBuildingFactory : IBuildingFactory
    {
        public Building CreateBuilding()
        {
            return new Farm(ConfigurationManager.GetInstance().GetWorkplaceBuildingMaxCapacity(typeof(Farm)),
                ConfigurationManager.GetInstance().GetBuildingBuildTime(typeof(Farm)));
        }
    }
    public class FarmBuildingRegister : IBuildingRegister
    {
        public IBuildingFactory CreateFactory()
        {
            return new FarmBuildingFactory();
        }

        public Type GetBuildingType()
        {
            return typeof(Farm);
        }
    }
}
